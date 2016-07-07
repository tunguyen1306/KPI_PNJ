Imports System.Web

Module Module1

    Sub Main()
        Console.WriteLine(Decode(" &lt;p&gt;Category : Quản trị ứng dụng_01.02.01&lt;br /&gt;br /&gt;&lt;/div&gt;&lt;p&gt;&lt;/p&gt;"))

    End Sub

    Public Function Decode(ByVal EncodedString As String) As String
        Return System.Web.HttpUtility.HtmlDecode(EncodedString)
    End Function

End Module