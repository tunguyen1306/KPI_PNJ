Imports System.Web

Module Module1

    Sub Main()
        Console.WriteLine(ConvertIntoDate(4, 1466405415))

    End Sub

    Public Function ConvertIntoDate(ByVal Condition As Int16, ByVal Number As Int32) As String

        Dim startDate As New DateTime(1970, 1, 1, 0, 0, 0)
        startDate = startDate.AddSeconds(Number)
        startDate = TimeZone.CurrentTimeZone.ToLocalTime(startDate)
        Select Case Condition
            Case 1
                Return startDate.ToString("dd-MM-yyyy")
            Case 2
                Return startDate.ToString("MM-yyyy")
            Case 3
                Return startDate.ToString("yyyy")
            Case 4
                Return DatePart(DateInterval.Quarter, startDate)
            Case Else
                Return startDate.ToString("dd-MM-yyyy")
        End Select
    End Function

End Module