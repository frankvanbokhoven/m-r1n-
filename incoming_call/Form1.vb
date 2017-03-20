Imports pjsip4net
Imports pjsip4net.Accounts
Imports pjsip4net.Calls
Imports pjsip4net.Configuration
Imports pjsip4net.Core
Imports pjsip4net.Core.Configuration
Imports pjsip4net.Core.Data
Imports pjsip4net.Interfaces
Imports pjsip.Interop
Imports log4net
Imports log4net.Config
Imports System
Imports System.Net
Imports System.Configuration
Imports pjsip4net.Core.Interfaces.ApiProviders
Imports pjsip4net.Core.Interfaces
Imports pjsip4net.Core.Utils

Module globals
    Public log As New Form1.dlg_logWrite(AddressOf Form1.logWrite)
End Module

Public Class Form1

#Region "Properties"
    Dim _myAccount As IAccount
    Private Property account As IAccount

    Dim myUa As ISipUserAgent
    Private Property ua As ISipUserAgent

    Private _registered As Boolean
#End Region

#Region "Log"
    Friend Delegate Function dlg_logWrite(ByVal text As String) As Boolean
    Friend Function logWrite(ByVal text As String) As Boolean
        If InvokeRequired Then
            Me.Invoke(New dlg_logWrite(AddressOf invLogWrite), New Object() {text})
        Else
            invLogWrite(text)
        End If
        Return True
    End Function
    Friend Function invLogWrite(ByVal text As String) As Boolean
        rtbMessages.AppendText(text)
        rtbMessages.ScrollToCaret()
        Return True
    End Function
#End Region

#Region "Pjsip4Net Event-Routines"
    Private Shared Sub intLog(ByVal sender As Object, ByVal e As LogEventArgs)
        Select Case e.Level
            Case 0
                log("FATAL: " & e.Data)
                Return
            Case 1
                log("Error: " & e.Data)
                Return
            Case 2
                log("Warn:  " & e.Data)
                Return
            Case 3
                log("Info:  " & e.Data)
                Return
            Case 4
            Case 5
                log("Debug: " & e.Data)
                Return
            Case Else
                Return
        End Select
    End Sub
    Private Shared Sub incomingCall(ByVal sender As Object, e As pjsip4net.Core.Utils.EventArgs(Of pjsip4net.Interfaces.ICall))
        log("Incoming Call from " & e.Data.RemoteInfo & vbCrLf)
    End Sub
    Private Shared Sub CallManager_CallStateChanged(ByVal sender As Object, ByVal e As CallStateChangedEventArgs)
        log(e.MediaState & " " & e.DestinationUri & " " & e.Duration.TotalMinutes & vbCrLf)
        log(e.Id & vbCrLf)
    End Sub
    Private Shared Sub Accounts_AccountStateChanged(ByVal sender As Object, ByVal e As AccountStateChangedEventArgs)
        log(vbCrLf & "Account State has changed!" & vbCrLf)
        log(e.StatusText & vbCrLf)
    End Sub
#End Region

#Region "SIP-Account Registration"
    Private Function register() As Boolean
        Try
            ua = BuildUserAgent.Start(BuildUserAgent.Build(ConfigureVersion_1_4.WithVersion_1_4(Configure.Pjsip4Net.With(New MyConfigurator))))
            AddHandler ua.Log, New EventHandler(Of LogEventArgs)(AddressOf intLog)
            AddHandler ua.CallManager.CallStateChanged, New EventHandler(Of CallStateChangedEventArgs)(AddressOf Form1.CallManager_CallStateChanged)
            AddHandler ua.AccountManager.AccountStateChanged, New EventHandler(Of AccountStateChangedEventArgs)(AddressOf Form1.Accounts_AccountStateChanged)
            AddHandler ua.CallManager.IncomingCall, New EventHandler(Of pjsip4net.Core.Utils.EventArgs(Of pjsip4net.Interfaces.ICall))(AddressOf Form1.incomingCall)
            Return True
        Catch ex As PjsipErrorException
            log(ex.Message)
            Return False
        Catch se As SystemException
            log(se.Message)
            Return False
        End Try
    End Function
    Private Sub unregister()
        RemoveHandler ua.Log, AddressOf intLog
        RemoveHandler ua.CallManager.CallStateChanged, AddressOf Form1.CallManager_CallStateChanged
        RemoveHandler ua.AccountManager.AccountStateChanged, AddressOf Form1.Accounts_AccountStateChanged
        RemoveHandler ua.CallManager.IncomingCall, AddressOf Form1.incomingCall
        ua.Dispose()
    End Sub
#End Region

#Region "Form Events"
    Private Sub btnRegister_Click(sender As System.Object, e As System.EventArgs) Handles btnRegister.Click
        btnRegister.Enabled = False
        If _registered Then
            Me.unregister()
            btnRegister.Text = "Register!"
            _registered = False
        Else
            If Me.register() Then
                btnRegister.Text = "Stop!"
                _registered = True
                log("Account successfully registered." & vbCrLf)
            Else
                log("Account could not be registered." & vbCrLf)
            End If
        End If
        btnRegister.Enabled = True
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _registered = False
        btnRegister.Text = "Register!"
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If _registered Then
            Me.unregister()
        End If
    End Sub
#End Region

End Class

Public Class MyConfigurator
    Implements IConfigurationProvider

    Public Sub Configure(ByVal context As IConfigurationContext) Implements pjsip4net.Core.Interfaces.IConfigurationProvider.Configure
        Dim registrar As String = "sip:" & Form1.txtDomain.Text
        Dim accountId As String = New SipUriBuilder().AppendDomain(Form1.txtDomain.Text).AppendExtension(Form1.txtUser.Text).ToString
        Dim proxy As String = "sip:" & Form1.txtProxy.Text

        Dim accountConfigArray As AccountConfig() = New AccountConfig(1 - 1) {}
        Dim accountConfig As AccountConfig = New AccountConfig()

        accountConfig.RegUri = registrar
        accountConfig.Id = accountId

        Dim networkCreds As List(Of NetworkCredential) = New List(Of NetworkCredential)()
        networkCreds.Add(New NetworkCredential(Form1.txtUser.Text, Form1.txtPasswd.Text, Form1.txtDomain.Text))
        accountConfig.Credentials = networkCreds

        Dim strs As List(Of String) = New List(Of String)()
        strs.Add(proxy)
        accountConfig.Proxy = strs

        accountConfigArray(0) = accountConfig
        context.RegisterAccounts(accountConfigArray)
    End Sub

End Class