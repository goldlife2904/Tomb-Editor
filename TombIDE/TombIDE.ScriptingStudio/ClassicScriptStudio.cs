﻿using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TombIDE.ScriptingStudio.Bases;
using TombIDE.ScriptingStudio.Controls;
using TombIDE.ScriptingStudio.Forms;
using TombIDE.ScriptingStudio.Objects;
using TombIDE.ScriptingStudio.ToolWindows;
using TombIDE.ScriptingStudio.UI;
using TombIDE.Shared;
using TombIDE.Shared.SharedClasses;
using TombLib.Scripting.Bases;
using TombLib.Scripting.ClassicScript;
using TombLib.Scripting.ClassicScript.Enums;
using TombLib.Scripting.ClassicScript.Objects;
using TombLib.Scripting.ClassicScript.Parsers;
using TombLib.Scripting.ClassicScript.Utils;
using TombLib.Scripting.ClassicScript.Writers;
using TombLib.Scripting.Enums;
using TombLib.Scripting.Interfaces;

namespace TombIDE.ScriptingStudio
{
	public sealed class ClassicScriptStudio : StudioBase
	{
		public override StudioMode StudioMode => StudioMode.ClassicScript;

		#region Fields

		private FormReferenceInfo FormReferenceInfo = new FormReferenceInfo();
		private FormNGCompilingStatus FormCompiling = new FormNGCompilingStatus();

		private BackgroundWorker NGCBackgroundWorker = new BackgroundWorker();

		public ReferenceBrowser ReferenceBrowser = new ReferenceBrowser();

		#endregion Fields

		#region Construction

		public ClassicScriptStudio() : base(IDE.Global.Project.ScriptPath, IDE.Global.Project.EnginePath)
		{
			DockPanelState = DefaultLayouts.ClassicScriptLayout;

			EditorTabControl.FileOpened += EditorTabControl_FileOpened;

			NGCBackgroundWorker.DoWork += NGCBackgroundWorker_DoWork;
			NGCBackgroundWorker.RunWorkerCompleted += NGCBackgroundWorker_RunWorkerCompleted;

			ReferenceBrowser.ReferenceDefinitionRequested += ReferenceBrowser_ReferenceDefinitionRequested;

			FileExplorer.Filter = "*.txt";

			EditorTabControl.PlainTextTypeOverride = typeof(ClassicScriptEditor);

			string initialFilePath = PathHelper.GetScriptFilePath(IDE.Global.Project.ScriptPath);

			if (!string.IsNullOrWhiteSpace(initialFilePath))
				EditorTabControl.OpenFile(initialFilePath);
		}

		#endregion Construction

		#region IDE Events

		protected override void OnIDEEventRaised(IIDEEvent obj)
		{
			base.OnIDEEventRaised(obj);

			IDEEvent_HandleSilentActions(obj);
		}

		private bool IsSilentAction(IIDEEvent obj)
			=> obj is IDE.ScriptEditor_AppendScriptLinesEvent
			|| obj is IDE.ScriptEditor_AddNewLevelStringEvent
			|| obj is IDE.ScriptEditor_AddNewPluginEntryEvent
			|| obj is IDE.ScriptEditor_AddNewNGStringEvent
			|| obj is IDE.ScriptEditor_ScriptPresenceCheckEvent
			|| obj is IDE.ScriptEditor_StringPresenceCheckEvent
			|| obj is IDE.ScriptEditor_RenameLevelEvent;

		private void IDEEvent_HandleSilentActions(IIDEEvent obj)
		{
			if (IsSilentAction(obj))
			{
				TabPage cachedTab = EditorTabControl.SelectedTab;

				TabPage scriptFileTab = EditorTabControl.FindTabPage(PathHelper.GetScriptFilePath(ScriptRootDirectoryPath));
				bool wasScriptFileAlreadyOpened = scriptFileTab != null;
				bool wasScriptFileFileChanged = wasScriptFileAlreadyOpened && EditorTabControl.GetEditorOfTab(scriptFileTab).IsContentChanged;

				TabPage languageFileTab = EditorTabControl.FindTabPage(PathHelper.GetLanguageFilePath(ScriptRootDirectoryPath, GameLanguage.English));
				bool wasLanguageFileAlreadyOpened = languageFileTab != null;
				bool wasLanguageFileFileChanged = wasLanguageFileAlreadyOpened && EditorTabControl.GetEditorOfTab(languageFileTab).IsContentChanged;

				if (obj is IDE.ScriptEditor_AppendScriptLinesEvent asle && asle.Lines.Count > 0)
				{
					AppendScriptLines(asle.Lines);
					EndSilentScriptAction(cachedTab, true, !wasScriptFileFileChanged, !wasScriptFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_AddNewLevelStringEvent anlse)
				{
					AddNewLevelNameString(anlse.LevelName);
					EndSilentScriptAction(cachedTab, true, !wasLanguageFileFileChanged, !wasLanguageFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_AddNewPluginEntryEvent anpee)
				{
					bool isChanged = AddNewPluginEntry(anpee.PluginString);
					EndSilentScriptAction(cachedTab, isChanged, !wasScriptFileFileChanged, !wasScriptFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_AddNewNGStringEvent anngse)
				{
					bool isChanged = AddNewNGString(anngse.NGString);
					EndSilentScriptAction(cachedTab, isChanged, !wasLanguageFileFileChanged, !wasLanguageFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_ScriptPresenceCheckEvent scrpce)
				{
					IDE.Global.ScriptDefined = IsLevelScriptDefined(scrpce.LevelName);
					EndSilentScriptAction(cachedTab, false, false, !wasScriptFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_StringPresenceCheckEvent strpce)
				{
					IDE.Global.StringDefined = IsLevelLanguageStringDefined(strpce.String);
					EndSilentScriptAction(cachedTab, false, false, !wasLanguageFileAlreadyOpened);
				}
				else if (obj is IDE.ScriptEditor_RenameLevelEvent rle)
				{
					string oldName = rle.OldName;
					string newName = rle.NewName;

					RenameRequestedLevelScript(oldName, newName);
					RenameRequestedLanguageString(oldName, newName);

					EndSilentScriptAction(cachedTab, true, !wasLanguageFileFileChanged, !wasLanguageFileAlreadyOpened);
				}
			}
		}

		private void AppendScriptLines(List<string> inputLines)
		{
			EditorTabControl.OpenFile(PathHelper.GetScriptFilePath(ScriptRootDirectoryPath));

			if (CurrentEditor is TextEditorBase editor)
			{
				editor.AppendText(string.Join(Environment.NewLine, inputLines) + Environment.NewLine);
				editor.ScrollToLine(editor.LineCount);
			}
		}

		private void AddNewLevelNameString(string levelName)
		{
			EditorTabControl.OpenFile(PathHelper.GetLanguageFilePath(ScriptRootDirectoryPath, GameLanguage.English), EditorType.Text);

			if (CurrentEditor is TextEditorBase editor)
				LanguageStringWriter.WriteNewLevelNameString(editor, levelName);
		}

		private bool AddNewPluginEntry(string pluginString)
		{
			EditorTabControl.OpenFile(PathHelper.GetScriptFilePath(ScriptRootDirectoryPath));

			if (CurrentEditor is ClassicScriptEditor editor)
				return editor.TryAddNewPluginEntry(pluginString);

			return false;
		}

		private bool AddNewNGString(string ngString)
		{
			EditorTabControl.OpenFile(PathHelper.GetLanguageFilePath(ScriptRootDirectoryPath, GameLanguage.English), EditorType.Text);

			if (CurrentEditor is TextEditorBase editor)
				return LanguageStringWriter.WriteNewNGString(editor, ngString);

			return false;
		}

		private void RenameRequestedLevelScript(string oldName, string newName)
		{
			EditorTabControl.OpenFile(PathHelper.GetScriptFilePath(ScriptRootDirectoryPath));

			if (CurrentEditor is TextEditorBase editor)
				ScriptReplacer.RenameLevelScript(editor, oldName, newName);
		}

		private void RenameRequestedLanguageString(string oldName, string newName)
		{
			EditorTabControl.OpenFile(PathHelper.GetLanguageFilePath(ScriptRootDirectoryPath, GameLanguage.English), EditorType.Text);

			if (CurrentEditor is TextEditorBase editor)
				ScriptReplacer.RenameLanguageString(editor, oldName, newName);
		}

		private bool IsLevelScriptDefined(string levelName)
		{
			EditorTabControl.OpenFile(PathHelper.GetScriptFilePath(ScriptRootDirectoryPath));

			if (CurrentEditor is TextEditorBase editor)
				return DocumentParser.IsLevelScriptDefined(editor.Document, levelName);

			return false;
		}

		private bool IsLevelLanguageStringDefined(string levelName)
		{
			EditorTabControl.OpenFile(PathHelper.GetLanguageFilePath(ScriptRootDirectoryPath, GameLanguage.English), EditorType.Text);

			if (CurrentEditor is TextEditorBase editor)
				return DocumentParser.IsLevelLanguageStringDefined(editor.Document, levelName);

			return false;
		}

		private void EndSilentScriptAction(TabPage previousTab, bool indicateChange, bool saveAffectedFile, bool closeAffectedTab)
		{
			if (indicateChange)
			{
				CurrentEditor.LastModified = DateTime.Now;
				IDE.Global.ScriptEditor_IndicateExternalChange();
			}

			if (saveAffectedFile)
				EditorTabControl.SaveFile(EditorTabControl.SelectedTab);

			if (closeAffectedTab)
				EditorTabControl.TabPages.Remove(EditorTabControl.SelectedTab);

			EditorTabControl.EnsureTabFileSynchronization();

			if (previousTab != null)
				EditorTabControl.SelectTab(previousTab);
		}

		#endregion IDE Events

		#region Events

		private void EditorTabControl_FileOpened(object sender, EventArgs e)
		{
			if (sender is ClassicScriptEditor textEditor)
			{
				textEditor.MouseDoubleClick += TextEditor_MouseDoubleClick;
				textEditor.KeyDown += TextEditor_KeyDown;
				textEditor.WordDefinitionRequested += TextEditor_WordDefinitionRequested;
			}
		}

		private void TextEditor_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left && ModifierKeys == Keys.Control)
				OpenIncludeFile();
		}

		private void TextEditor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.F5)
				OpenIncludeFile();
		}

		private void TextEditor_WordDefinitionRequested(object sender, WordDefinitionEventArgs e)
		{
			string word = e.Word;

			ReferenceType type = ReferenceType.MnemonicConstant;

			if (e.Type == WordType.Header)
			{
				word = "[" + word + "]";
				type = ReferenceType.OldCommand;
			}
			else if (e.Type == WordType.Command)
				type = RddaReader.GetCommandType(word);

			FormReferenceInfo.Show(word, type);
		}

		private void NGCBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Process ngCenterProcess = Array.Find(Process.GetProcesses(), x => x.ProcessName.Contains("NG_Center"));

			if (ngCenterProcess != null)
				ngCenterProcess.WaitForExit();
		}

		private void NGCBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (FormCompiling.Visible)
				FormCompiling.Close();
		}

		private void ReferenceBrowser_ReferenceDefinitionRequested(object sender, ReferenceDefinitionEventArgs e)
			=> FormReferenceInfo.Show(e.Keyword, e.Type);

		#endregion Events

		#region Other methods

		private void OpenIncludeFile()
		{
			if (CurrentEditor is TextEditorBase editor)
			{
				string fullFilePath = CommandParser.GetFullIncludePath(editor.Document, editor.CaretOffset);

				if (File.Exists(fullFilePath))
					EditorTabControl.OpenFile(fullFilePath);
				else
					DarkMessageBox.Show(this, "Couldn't find the target file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				editor.SelectionLength = 0;
			}
		}

		private async void CompileTRNGScript()
		{
			EditorTabControl.SaveAll();

			try
			{
				FormCompiling.ShowCompilingMode();
				FormCompiling.Show();

				bool success = await NGCompiler.Compile(
					ScriptRootDirectoryPath, EngineDirectoryPath,
					IDE.Global.IDEConfiguration.UseNewIncludeMethod);

				if (success)
				{
					string logFilePath = Path.Combine(DefaultPaths.VGEScriptDirectory, "script_log.txt");
					CompilerLogs.UpdateLogs(File.ReadAllText(logFilePath));

					if (IDE.Global.IDEConfiguration.ShowCompilerLogsAfterBuild)
						CompilerLogs.DockGroup.SetVisibleContent(CompilerLogs);

					if (FormCompiling.Visible)
						FormCompiling.Close();
				}
				else
				{
					FormCompiling.ShowDebugMode();

					if (!NGCBackgroundWorker.IsBusy)
						NGCBackgroundWorker.RunWorkerAsync();
				}
			}
			catch (Exception ex)
			{
				if (FormCompiling.Visible)
					FormCompiling.Close();

				DarkMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		protected override void ApplyUserSettings(IEditorControl editor)
			=> editor.UpdateSettings(Configs.ClassicScript);

		protected override void ApplyUserSettings()
		{
			foreach (TabPage tab in EditorTabControl.TabPages)
				ApplyUserSettings(EditorTabControl.GetEditorOfTab(tab));

			StatusStrip.SyntaxPreview.ReloadSettings();

			UpdateSettings();
		}

		protected override void Build()
			=> CompileTRNGScript();

		#endregion Other methods
	}
}
