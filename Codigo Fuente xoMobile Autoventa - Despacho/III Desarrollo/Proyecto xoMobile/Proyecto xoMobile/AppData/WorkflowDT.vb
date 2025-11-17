Imports System.Data
Imports System.Data.SqlServerCe

Public Class WorkflowDT

    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient

    '--OK
    Public Function getWorkflow(ByVal idCliente As String, ByRef rlayer As rLayerHandler) As DataTable

        SQL_QUERY = "  " _
        & " SELECT DISTINCT showvalue, datavalue, tabla, toperacion, CASE WHEN  toperacion IS null THEN  0 ELSE 1 END as realizado  " _
        & "   FROM   " _
        & "   (  " _
        & "   	SELECT * FROM TTIPO  " _
        & "   	WHERE tabla in ('WORKFLOW','OPTIONAL_WORKFLOW')    " _
        & "   )A  " _
        & "   left join   " _
        & "   (  " _
        & "   	SELECT DISTINCT  " _
        & " 	CASE  " _
        & " 	when toperacion   = 3 	then 'Cobro' " _
        & " 	when toperacion   = 5 	then 'Venta'  " _
        & " 	when toperacion   = 7 	then 'Encuesta'  " _
        & " 	when toperacion   = 46  then 'Despacho'   " _
        & " 	when toperacion   = 1 	then 'Inventario' end as operacion ,  toperacion " _
        & " 	FROM CBITACORA " _
        & " 	WHERE idcliente = " & idCliente & " and toperacion in (3,5,7,46,1) " _
        & "   )B  " _
        & "   ON operacion =  showValue   " _
        & "   order by datavalue  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    '--OK
    Public Function getWorkflow2(ByVal idCliente As String, ByRef rlayer As rLayerHandler) As DataTable

        SQL_QUERY = "  " _
        & " SELECT DISTINCT A.nvalue,A.toperacion, A.tabla, B.toperacion, CASE WHEN B.toperacion IS null THEN  0 ELSE 1 END as realizado  " _
        & "   FROM   " _
        & "   (  " _
        & "   	SELECT g.* FROM rworkflow_giro g,rcliente c " _
        & "   	WHERE c.id_cliente = " & idCliente & " AND  g.giro = c.ramo" _
        & "   )A  " _
        & "   left join   " _
        & "   (  " _
        & "   	SELECT DISTINCT  " _
        & " 	CASE  " _
        & " 	when toperacion   = 3 	then 'Cobro' " _
        & " 	when toperacion   = 5 	then 'Venta'  " _
        & " 	when toperacion   = 46  then 'Despacho'   " _
        & " 	when toperacion   = 7 	then 'Encuesta'  " _
        & " 	when toperacion   = 1 	then 'Inventario' end as operacion ,  toperacion " _
        & " 	FROM CBITACORA " _
        & " 	WHERE idcliente = " & idCliente & " " _
        & "   )B  " _
        & "   ON B.operacion =  A.nvalue " _
        & "   where A.nvalue <> 'Despacho' " _
        & "   order by A.toperacion  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT

    End Function

    Public Function getWorkflow3(ByVal idCliente As String, ByRef rlayer As rLayerHandler) As DataTable

        SQL_QUERY = "  " _
        & " SELECT DISTINCT A.nvalue,A.toperacion, A.tabla, B.toperacion, CASE WHEN B.toperacion IS null THEN  0 ELSE 1 END as realizado  " _
        & "   FROM   " _
        & "   (  " _
        & "   	SELECT g.* FROM rworkflow_giro g,rcliente c " _
        & "   	WHERE c.id_cliente = " & idCliente & " AND  g.giro = c.ramo" _
        & "   )A  " _
        & "   left join   " _
        & "   (  " _
        & "   	SELECT DISTINCT  " _
        & " 	CASE  " _
        & " 	when toperacion   = 3 	then 'Cobro' " _
        & " 	when toperacion   = 7 	then 'Encuesta'  " _
        & " 	when toperacion   = 5 	then 'Venta'  " _
        & " 	when toperacion   = 46  then 'Despacho'   " _
        & " 	when toperacion   = 1 	then 'Inventario' end as operacion ,  toperacion " _
        & " 	FROM CBITACORA " _
        & " 	WHERE idcliente = " & idCliente & " " _
        & "   )B  " _
        & "   ON B.operacion =  A.nvalue " _
        & "   where A.nvalue NOT IN  ('Venta','Encuesta','Inventario') " _
        & "   order by A.toperacion  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function


End Class
