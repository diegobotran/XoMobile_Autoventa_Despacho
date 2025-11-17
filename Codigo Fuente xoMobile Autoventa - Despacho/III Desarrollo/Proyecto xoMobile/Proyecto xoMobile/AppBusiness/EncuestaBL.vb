Imports System.Data
'---ok
Public Class EncuestaBL
    Dim objEncuestas As New Encuesta
    Public Function getListadoEncuestas() As DataTable
        Dim dtEncuesta As New DataTable
        dtEncuesta = objEncuestas.getListado()
        If dtEncuesta.Rows.Count() <= 0 Then
            Throw New Exception("No se han cargado encuestas.")
        End If
        Return dtEncuesta
    End Function

    Public Function getListadoPreguntas(ByVal idEncuesta As String) As DataTable
        Dim dtEncuestaPreguntas As New DataTable
        Try
            dtEncuestaPreguntas = objEncuestas.getListadoPreguntas(idEncuesta)
            If dtEncuestaPreguntas.Rows.Count() <= 0 Then
                Throw New Exception("No se han cargado preguntas de encuestas.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dtEncuestaPreguntas
    End Function

    Public Function obtenerRespuestasAlternativas(ByVal idPregunta As String) As DataTable
        Dim dtRespuestas As New DataTable
        Dim objEncuesta As New Encuesta
        dtRespuestas = objEncuesta.getAlternativas(idPregunta)
        If dtRespuestas.Rows.Count > 0 Then
            Return dtRespuestas
        Else
            Throw New Exception("No hay rutas cargadas")
        End If
    End Function

    Public Function agregarRespuesta(ByVal idEncEncuesta As String, ByVal idEncuesta As String, ByVal idtema As String, ByVal idPregunta As String, ByVal linea As String, ByVal respuesta As String) As Boolean
        Dim objEncuesta As New Encuesta
        If objEncuesta.agregarRespuesta(idEncEncuesta, idEncuesta, idtema, idPregunta, linea, respuesta) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function agregarEncabezadoEncuesta(ByVal idEncuesta As String) As String
        Dim objEncuesta As New Encuesta
        Try
            Return objEncuesta.agregarEncabezadoEncuesta(idEncuesta)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function
End Class
