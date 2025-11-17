Imports System.Data

Public Class rLayerHandler
    Dim texto_ As String
    Dim codigo_ As Integer
    Dim sql_ As String
    Dim enteroEscalar_ As String
    Dim conError_ As Boolean
    '---ok

    Public Property conError() As Boolean
        Get
            ' Return the value stored in the local variable.
            Return conError_
        End Get

        Set(ByVal Value As Boolean)
            ' Store the value in a local variable.
            conError_ = Value
        End Set
    End Property

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
            Return sql_
        End Get

        Set(ByVal Value As String)
            ' Store the value in a local variable.
            sql_ = Value
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

    Public Function evaluarError(Optional ByVal raise As Boolean = False) As Boolean
        If raise Then
            If codigo = 1 Then
                MsgBox(texto)
            End If
            If codigo = 100 Then
                MsgBox("Erro capa de datos: " & texto & vbCrLf & SQL)
            End If
        End If
        

        If codigo = 500 Then
            '--- Codigo de aborto de proceso
        End If
        Return True
    End Function

    Public Function evaluaTabla(ByVal dtGeneric As DataTable) As Boolean

        If Not dtGeneric Is Nothing Then
            If dtGeneric.Rows.Count > 0 Then
                Return True
            Else
                codigo = 100
                texto = "No se encontro el codigo en el listado"
                conError = True
                Return False
            End If
        Else
            codigo = 100
            texto = "No se encontro el codigo en el listado"
            conError = True
            Return False
        End If
    End Function
End Class
