Public Class rLayerHandler
    Dim texto_ As String
    Dim codigo_ As Integer
    Dim sql_ As String
    Dim enteroEscalar_ As String
    '---ok

    Public Property codigo() As Integer
        Get
            ' Return the value stored in the local variable.
            Return codigo_
        End Get

        Set(ByVal Value As Integer)
            ' Store the value in a local variable.
            codigo_ = Value
        End Set
    End Property

    Public Property texto() As String
        Get
            ' Return the value stored in the local variable.
            Return texto_
        End Get

        Set(ByVal Value As String)
            ' Store the value in a local variable.
            texto_ = Value
        End Set
    End Property

    Public Property SQL() As String
        Get
            ' Return the value stored in the local variable.
            Return SQL_
        End Get

        Set(ByVal Value As String)
            ' Store the value in a local variable.
            SQL_ = Value
        End Set
    End Property

    'Public Property enteroEscalar() As String
    '    Get
    '        ' Return the value stored in the local variable.
    '        Return enteroEscalar_
    '    End Get

    '    Set(ByVal Value As String)
    '        ' Store the value in a local variable.
    '        enteroEscalar_ = Value
    '    End Set
    'End Property

    Public Function evaluarError() As Boolean
        If codigo = 1 Then
            MsgBox(texto)
        End If

        If codigo = 100 Then
            MsgBox("Erro capa de datos: " & vbCrLf & SQL)
        End If

        If codigo = 500 Then
            '--- Codigo de aborto de proceso
        End If
        Return True
    End Function
End Class
