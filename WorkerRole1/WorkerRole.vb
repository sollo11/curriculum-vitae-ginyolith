Imports System.Net
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.WindowsAzure.ServiceRuntime

Public Class WorkerRole
    Inherits RoleEntryPoint

    Private cancellationTokenSource As CancellationTokenSource = New CancellationTokenSource
    Private runCompleteEvent As ManualResetEvent = New ManualResetEvent(False)

    Public Overrides Sub Run()
        Trace.TraceInformation("WorkerRole1 is running")

        Try
            Me.RunAsync(Me.cancellationTokenSource.Token).Wait()
        Finally
            Me.runCompleteEvent.Set()
        End Try
    End Sub

    Public Overrides Function OnStart() As Boolean
        ' Nastavte maximální počet souběžných připojení.
        ServicePointManager.DefaultConnectionLimit = 12

        ' Informace o zpracování změn konfigurace
        ' najdete v tématu MSDN na adrese https://go.microsoft.com/fwlink/?LinkId=166357.
        Dim result As Boolean = MyBase.OnStart()

        Trace.TraceInformation("WorkerRole1 has been started")

        Return result
    End Function

    Public Overrides Sub OnStop()

        Trace.TraceInformation("WorkerRole1 is stopping")

        Me.cancellationTokenSource.Cancel()
        Me.runCompleteEvent.WaitOne()

        MyBase.OnStop()

        Trace.TraceInformation("WorkerRole1 has stopped")

    End Sub

    Private Async Function RunAsync(ByVal cancellationToken As CancellationToken) As Task

        ' TODO: Nahradit následující kód vlastní logikou
        While Not cancellationToken.IsCancellationRequested
            Trace.TraceInformation("Working")
            Await Task.Delay(1000)
        End While

    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
