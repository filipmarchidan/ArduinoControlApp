Imports System.IO
Imports System.IO.Ports
Imports System.Threading


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Try


            For Each port As String In SerialPort.GetPortNames
                ComboBox1.Items.Add(port)
            Next
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedItem = "9600"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If Me.WindowState = FormWindowState.Maximized Then

        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Connect" Then
            Try
                SerialPort1.BaudRate = ComboBox2.SelectedItem
                SerialPort1.PortName = ComboBox1.SelectedItem
                SerialPort1.Open()
                Button1.Text = "Disconnect"
                TextBox1.Enabled = True
                ComboBox1.Enabled = False
                ComboBox2.Enabled = False
            Catch ex As Exception

            End Try
        Else
            SerialPort1.Close()
            Button1.Text = "Connect"
            TextBox1.Enabled = False
            ComboBox1.Enabled = True
            ComboBox2.Enabled = True
        End If
       
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SerialPort1.Write(TextBox1.Text)
            TextBox1.Clear()
        End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        RichTextBox1.Text &= SerialPort1.ReadExisting
        RichTextBox1.AppendText(SerialPort1.ReadExisting)
        RichTextBox1.SelectionStart = RichTextBox1.TextLength
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CreditsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreditsToolStripMenuItem.Click
        MsgBox("This program has been created by Filip Marchidan to ease the control of Arduino and provide a friendly interface with the console" + Environment.NewLine + "Copyright ©2017 Filip Marchidan v1.0", MessageBoxIcon.Information, MessageBoxButtons.OKCancel)
    End Sub

    Private Sub SaveLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveLogToolStripMenuItem.Click
        sfd.Filter = "Plain Text|*.txt|Word Document|*.docx|PDF|*.pdf|Excel Document|*.xls|PowerPoint Presentation|*.pptx"
        sfd.Title = "Save File"
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            RichTextBox1.SaveFile(sfd.FileName)
        Else
            MsgBox("An error has ocurred", MessageBoxOptions.ServiceNotification, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CommandsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommandsToolStripMenuItem.Click
        Form2.Show()
    End Sub
End Class
