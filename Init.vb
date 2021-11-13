﻿Public Class Init
    Public Shared Sub Initialization()
        InitFile()
        InitDrops()
        InitLabels()
        InitRich()
    End Sub
    Private Shared Sub InitFile()
        CTGVidX.VidX.Prep()
        Mem.UpdaterD = ClarkTribeGames.MySQLReader.Query(LCase(Mem.Updater).Replace(".exe", ""), "d")
        If System.IO.File.Exists(Mem.Updater) Then
            If System.IO.File.GetLastWriteTime(Mem.Updater) < Convert.ToDateTime(Mem.UpdaterD) Then
                System.IO.File.Delete(Mem.Updater)
                ClarkTribeGames.Updater.GetUpdater()
            End If
        Else
            ClarkTribeGames.Updater.GetUpdater()
        End If
        If System.IO.File.Exists(Mem.Updater) Then
            Dim ToHideFile As New System.IO.FileInfo(Mem.Updater) With {
                .Attributes = IO.FileAttributes.Hidden}
        End If
    End Sub
    Private Shared Sub InitDrops()
        Main.TypeDrop.Items.Clear()
        For Each type In Mem.AV
            Main.TypeDrop.Items.Add(type)
        Next
        Main.ExtDrop.Items.Clear()
        Main.TypeDrop.SelectedIndex = 0
    End Sub
    Private Shared Sub InitRich()
        Main.LogBox.Text = Main.AppLabel.Text & Main.VerLabel.Text & " Starting..."
    End Sub
    Private Shared Sub InitLabels()
        Dim Title As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
        Main.Text = Title
        Main.Icon = My.Resources.logo
        Main.AppLabel.Text = Title & " v"
        Dim VersionParts() As String = Strings.Split((System.Reflection.Assembly.
            GetExecutingAssembly().GetName().Version.ToString()), ".", 4)
        Mem.Current = VersionParts(0) & "." & VersionParts(1) & "." & Action.
            VersionConverter(VersionParts(2), 3) & "." & Action.VersionConverter(VersionParts(3), 4)
        Main.VerLabel.Text = Mem.Current
        Mem.Available = Mem.UpdaterD = ClarkTribeGames.MySQLReader.Query(LCase(Mem.Updater).Replace(".exe", ""), "v")
        Mem.UpdaterU = Mem.UpdaterD = ClarkTribeGames.MySQLReader.Query(LCase(Mem.Updater).Replace(".exe", ""), "u")
        If Updater.Checker(Mem.Current, Mem.Available) = True Then
            Dim Answer As Integer = MsgBox("Update " & Mem.Available & " Available!" & vbCrLf & vbCrLf & "Would you like to update now?", vbYesNo + vbExclamation)
            If Answer = vbYes Then Updater.InstallUpdate() Else MsgBox("Please update as soon as possible!")
        End If
    End Sub
End Class
