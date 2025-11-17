Imports System.Data
Imports System.Data.SqlServerCe

Public Class Encuesta
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient
    '---ok
    'Public Function getEncuesta() As DataTable
    '    SQL_QUERY = " SELECT  * FROM CENCUESTA WHERE idRuta = " + id_glo_ruta.ToString()
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    'Public Function eliminar() As Boolean
    '    SQL_QUERY = " DELETE FROM RENCUESTA_PREGUNTAS "
    '    objCe.SetExecute(SQL_QUERY)
    '    SQL_QUERY = " DELETE FROM RENCUESTA_TEMAS "
    '    objCe.SetExecute(SQL_QUERY)
    '    SQL_QUERY = " DELETE FROM RENCUESTA "
    '    objCe.SetExecute(SQL_QUERY)
    'End Function

    Public Function getListado() As DataTable
        SQL_QUERY = _
        " SELECT R.*, CASE WHEN id_cEncuesta is null then 0 else 1 end as estado  " _
        + " FROM RENCUESTA R " _
        + " left join cencuesta  C " _
        + " on idEncuesta = id_Encuesta " _
        + " and idCliente =   " + id_glo_cliente.ToString()
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getListadoPreguntas(ByVal idEncuesta As String) As DataTable
        SQL_QUERY = _
        "   select T.idencuesta,P.idtema,P.id_pregunta,T.tema,P.pregunta,P.orden,P.tipoRespuesta   " _
        + " from rencuesta E " _
        + " left join  	rencuesta_temas   T " _
        + " on id_encuesta = idencuesta " _
        + " left join     rencuesta_preguntas P " _
        + " on 	T.id_tema= P.idTema   " _
        + " and	T.idEncuesta = P.idEncuesta " _
        + " where E.id_encuesta =  " + idEncuesta + " and T.idencuesta = " + idEncuesta + " and P.idencuesta =  " + idEncuesta _
        + " order by P.idTema, P.orden "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getAlternativas(ByVal idPregunta As String) As DataTable
        SQL_QUERY = _
        "   SELECT 0 id_alternativa,0 idPregunta,'-- Seleccione --' alternativa union  Select * from RENCUESTA_ALTERNATIVAS WHERE idPregunta = " + idPregunta
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function agregarRespuesta(ByVal idEncEncuesta As String, ByVal idEncuesta As String, ByVal idtema As String, ByVal idPregunta As String, ByVal linea As String, ByVal respuesta As String) As Integer
        SQL_QUERY = " INSERT INTO [CENCUESTA_DETALLE]([idEncEncuesta],[idEncuesta],[idTema],[idPregunta],[fecha],[linea],[respuesta])  " _
        + " VALUES ('" + idEncEncuesta + "','" + idEncuesta + "','" + idtema + "','" + idPregunta + "',GETDATE(),'" + linea + "','" + respuesta + "') "
        Return objCe.SetExecute(SQL_QUERY)
    End Function

    Public Function agregarEncabezadoEncuesta(ByVal idEncuesta As String) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Dim dtIdentity As New DataTable
        SQL_QUERY = " INSERT INTO [CENCUESTA]([idEncuesta],[idRuta],[idCliente],[idUsuario],[fecha]) " _
        + " VALUES ('" + idEncuesta + "','" + id_glo_ruta.ToString + "','" + id_glo_cliente.ToString + "','" + id_glo_usuario.ToString + "',GETDATE()) "

        Conexion.Open()
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT @@IDENTITY as nn"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Cerrar la conexion
        Conexion.Close()

        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString

    End Function
End Class
