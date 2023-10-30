Imports System.Net.Http
Imports System.Web.Script.Serialization
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Newtonsoft.Json

Public Class PCClient
    Dim jss As New JavaScriptSerializer()
    Dim GetTimeUrl As String = "https://pcconnect.adamkhattab.co.uk/api/time.php" ' Replace with the actual URL of your PHP script
    Dim PostTimeURL As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/updatepctimedatabase.php" ' Replace with the actual URL of your PHP script
    Dim findRequests As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/findrequests.php"
    Dim updateRequest As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/updaterequest.php"
    Dim getReminder As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/getreminder.php"
    Dim listReminderURL As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/listreminders.php"
    Dim checkInternetURL As String = "https://pcconnect.adamkhattab.co.uk/api/pcclient/checkinternet.php"
    Dim Operations As New Dictionary(Of String, Action)()
    Declare Function SetSystemPowerState Lib "kernel32" (ByVal fSuspend As Integer, ByVal fForce As Integer) As Integer
    Public apiKey As String
    Dim reminderTime, reminderListTime As String
    Dim reminderDate, reminderListDate As String
    Dim Tray As NotifyIcon
    Public ExitOption, Usercontrol, ControlPanelItem, AddPCItem As ToolStripItem
    Public ContextMenu As ContextMenuStrip
    Dim Output As String
    Dim reminderText, reminderListText, ReminderListTextBox, Completed As String
    Dim ID As String
    Public PCName As String

    Public Function logoutPCClient()
        ControlPanel.Close()
        Login.Close()
        AddPC.Close()
        My.Settings.Username = ""
        My.Settings.Password = ""
        My.Settings.PCName = ""
        My.Settings.Save()
        apiKey = ""
        PCName = ""
        Usercontrol.Text = "Login"
        ContextMenu.Items.Remove(ControlPanelItem)
        Login.Show()

    End Function

    Public Sub ControlPanelItem_Click(sender As Object, e As EventArgs)
        ControlPanel.Show()
    End Sub
    Private Sub ExitOption_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub Usercontrol_Click(sender As Object, e As EventArgs)
        If My.Settings.Username = "" Or My.Settings.Password = "" Then
            Login.Show()


        Else
            logoutPCClient()

        End If
    End Sub
    Public Sub AddPCItem_Click(sender As Object, e As EventArgs)
        If AddPC.Visible = True Then
            Exit Sub
        End If
        AddPC.ShowDialog()
    End Sub

    Public Sub PCClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        If My.Settings.FirstRun = True Then

            MsgBox("PCClient is now running in the background. To access the control panel, right click on the PCClient icon in the taskbar and click on Control Panel. To exit PCClient, right click on the PCClient icon in the taskbar and click on Exit.")

            My.Settings.FirstRun = False
            My.Settings.Save()
        End If
        Tray = New NotifyIcon
        Tray.Visible = True
        Tray.Icon = New Icon("PCClient Task Bar Logo.ico")
        Tray.Text = "PCClient"
        ContextMenu = New ContextMenuStrip
        If My.Settings.Username = "" Or My.Settings.Password = "" Then
            Usercontrol = ContextMenu.Items.Add("Login")
        Else
            Usercontrol = ContextMenu.Items.Add("Logout")
            If My.Settings.PCName = "" Then
            Else
                ControlPanelItem = ContextMenu.Items.Add("Control Panel")
                AddHandler ControlPanelItem.Click, AddressOf ControlPanelItem_Click
            End If
        End If

        ExitOption = ContextMenu.Items.Add("Exit")

        AddHandler ExitOption.Click, AddressOf ExitOption_Click
        AddHandler Usercontrol.Click, AddressOf Usercontrol_Click


        Tray.ContextMenuStrip = ContextMenu


        Tray.BalloonTipIcon = ToolTipIcon.Info
        Tray.BalloonTipTitle = "PCClient"
        Tray.BalloonTipText = "PCClient is running"

        Tray.ShowBalloonTip(50000)

        ShowInTaskbar = False

        If My.Settings.Username = "" Or My.Settings.Password = "" Then
            Login.Show()

        Else
            Output = Login.LoginFunction(My.Settings.Username, My.Settings.Password)
            If Output <> "Invalid username or password." Then
                apiKey = Output
                If My.Settings.PCName = "" Then
                    AddPC.ShowDialog()
                Else
                    PCName = My.Settings.PCName
                End If
                Running()
            ElseIf Output = "Invalid username or password." Then
                Login.Show()
            ElseIf Output.Contains("Error") Then
                MsgBox(Output)

            End If
        End If

        Operations("Sleep") = AddressOf Sleep
        Operations("Hibernate") = AddressOf Hibernate
        Operations("Shutdown") = AddressOf Shutdown
        Operations("Lock") = AddressOf Lock
        Operations("Signout") = AddressOf Signout
    End Sub

    Async Function Sleep() As Task
        Application.SetSuspendState(PowerState.Suspend, True, True)


    End Function
    Async Function Hibernate() As Task
        Application.SetSuspendState(PowerState.Hibernate, True, True)


    End Function
    Async Function Shutdown() As Task


        System.Diagnostics.Process.Start("shutdown", "-s -f -t 10")


    End Function
    Async Function Lock() As Task

        Process.Start("rundll32.exe", "user32.dll,LockWorkStation")


    End Function
    Async Function Signout() As Task

        Process.Start("shutdown", "/l")

    End Function
    Async Function GetRemindersFunction() As Task
        Using httpClient As New HttpClient()
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey)
            httpClient.DefaultRequestHeaders.Add("PCName", PCName)

            Dim GetRemindersResponse As HttpResponseMessage = Await httpClient.GetAsync(listReminderURL)
            Dim GetRemindersContent As String = GetRemindersResponse.Content.ReadAsStringAsync().Result
            Dim reminders As List(Of ReminderData) = JsonConvert.DeserializeObject(Of List(Of ReminderData))(GetRemindersContent)

            For Each listReminder As ReminderData In reminders
                Dim ListDate As String = listReminder.Date
                Dim ListTime As String = listReminder.Time
                Dim ListReminderText As String = listReminder.Reminder
                Dim ListCompleted As Integer = listReminder.Completed
                If ListCompleted = 1 Then
                    Completed = "Yes"
                Else
                    Completed = "No"
                End If
                ReminderListTextBox = ReminderListTextBox & "Date: " & ListDate & vbNewLine & "Time: " & ListTime & vbNewLine & "Reminder: " & ListReminderText & vbNewLine & "Completed: " & ListCompleted & vbNewLine & vbNewLine
                ' You can now use the date, time, and reminderText as needed

            Next
        End Using
    End Function
    Async Function Running() As Task
        Using httpClient As New HttpClient()
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey)
            httpClient.DefaultRequestHeaders.Add("PCName", PCName)
            GetRemindersFunction()
            ControlPanel.RemindersTextEdit(ReminderListTextBox)

            While True
                If My.Settings.Username = "" Or My.Settings.Password = "" Then
                    Exit While
                    Exit Function
                End If
                If My.Settings.PCName = "" Then
                    AddPC.ShowDialog()
                    Exit While
                    Exit Function
                End If
                Try

                    Dim GetTimeResponse As HttpResponseMessage = Await httpClient.GetAsync(GetTimeUrl)
                    Dim GetTimeResponseContent As String = Await GetTimeResponse.Content.ReadAsStringAsync()
                    'MsgBox(GetTimeResponseContent)
                    Dim TimeJSON As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(GetTimeResponseContent)
                    Dim Time As String = TimeJSON("time")

                    Dim TimePostData As New List(Of KeyValuePair(Of String, String))
                    TimePostData.Add(New KeyValuePair(Of String, String)("time", Time))
                    Dim TimeContent As New FormUrlEncodedContent(TimePostData)

                    Dim TimeResponse As HttpResponseMessage = Await httpClient.PostAsync(PostTimeURL, TimeContent)
                    Dim TimeResponseContent As String = Await TimeResponse.Content.ReadAsStringAsync()



                    Dim CheckInternet As HttpResponseMessage = Await httpClient.GetAsync(checkInternetURL)
                    Dim CheckInternetContent As String = Await CheckInternet.Content.ReadAsStringAsync()
                    'MsgBox(GetTimeResponseContent)
                    'please make a code to do a get request to findRequests and do a messagebox with the data got back

                    If CheckInternetContent = "yes" Then
                        If ControlPanel.Internet = 0 Then
                            ControlPanel.internetChange(1)

                        End If

                    ElseIf CheckInternetContent = "no" Then
                        If ControlPanel.Internet = 1 Then
                            ControlPanel.internetChange(0)

                        End If
                    Else
                        If ControlPanel.Internet = 1 Then
                            ControlPanel.internetChange(0)

                        End If
                    End If


                    Dim findRequestsResponse As HttpResponseMessage = Await httpClient.GetAsync(findRequests)
                    Dim findRequestsResponseContent As String = Await findRequestsResponse.Content.ReadAsStringAsync()
                    If Operations.ContainsKey(findRequestsResponseContent) Then
                        Dim updateRequestsResponse As HttpResponseMessage = Await httpClient.GetAsync(updateRequest)
                        Operations(findRequestsResponseContent).Invoke()
                    End If


                    'MsgBox(reminderTime)
                    Dim GetRemindersResponse As HttpResponseMessage = Await httpClient.GetAsync(listReminderURL)
                    Dim GetRemindersContent As String = GetRemindersResponse.Content.ReadAsStringAsync().Result
                    Dim reminders As List(Of ReminderData) = JsonConvert.DeserializeObject(Of List(Of ReminderData))(GetRemindersContent)
                    GetRemindersFunction()
                    If ControlPanel.RemindersT.Text = ReminderListTextBox Then
                    Else
                        ReminderListTextBox = ""
                        For Each listReminder As ReminderData In reminders
                            Dim ListDate As String = listReminder.Date
                            Dim ListTime As String = listReminder.Time
                            Dim ListReminderText As String = listReminder.Reminder
                            Dim ListCompleted As Integer = listReminder.Completed
                            If ListCompleted = 1 Then
                                Completed = "Yes"
                            Else
                                Completed = "No"
                            End If
                            ReminderListTextBox = ReminderListTextBox & "Date: " & ListDate & vbNewLine & "Time: " & ListTime & vbNewLine & "Reminder: " & ListReminderText & vbNewLine & "Completed: " & Completed & vbNewLine & vbNewLine
                            ' You can now use the date, time, and reminderText as needed

                        Next
                        ControlPanel.RemindersTextEdit(ReminderListTextBox)
                    End If
                    Dim GetReminders As HttpResponseMessage = Await httpClient.GetAsync(getReminder)
                    'MsgBox(GetReminders.Content.ReadAsStringAsync().Result)
                    'declare a variable to store the reminder time
                    Dim reminder As String = GetReminders.Content.ReadAsStringAsync().Result
                    Dim ReminderJSON As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(reminder)
                    reminderTime = ReminderJSON("time")
                    reminderDate = ReminderJSON("date")
                    reminderText = ReminderJSON("reminder")
                    ID = ReminderJSON("id")

                    If TimeOfDay >= reminderTime And Date.Today >= reminderDate Then
                        reminderWindow.Running(reminderText, ID)
                        'MsgBox("Reminder" & reminderText)
                    End If

                Catch ex As Exception
                    If ex.Message.Contains("An error occurred while sending the request.") Then
                        ControlPanel.internetChange(0)

                    ElseIf Not ex.Message.Contains("System.Collections.Generic.Dictionary") Then
                        MsgBox("Error: " & ex.Message)

                    End If

                End Try

                ' Add a delay to avoid excessive requests
                Await Task.Delay(TimeSpan.FromSeconds(0.5)) ' Adjust the delay interval as needed
            End While
        End Using
    End Function

End Class
