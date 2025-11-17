Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class Ruta
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable


    Dim objCe As New ceClient


    Public Function getDiferencias(ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = "" _
                 & " select  id_Diferencia, valor, idMotivo, tipoMotivo , CASE WHEN T1.SHOWVALUE IS NULL THEN T2.SHOWVALUE ELSE T1.SHOWVALUE  END AS MOTIVO  " _
                 & " from [CDIFERENCIA_LIQUIDACION]d " _
                 & " left join ttipo T1 on T1.datavalue = idmotivo and T1.tabla='FALTANTES' " _
                 & " left join ttipo  T2 on T2.datavalue = idmotivo and T2.tabla='SOBRANTES' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getFElDoctoHH() As DataTable
        SQL_QUERY = "SELECT 'X' from cfactura where serie is null and estado = 1 and tTipo in ('ODV','ZTAE') UNION ALL SELECT 'X' from cnotacredito where serie is null and estado = 1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getFElDoctoHHNC() As DataTable
        SQL_QUERY = "SELECT * from cnotacredito where serie is null and estado = 1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNPedidos() As DataTable
        SQL_QUERY = "select showValue as npedidos from ttipo where tabla LIKE '%FEL173_MAX_PEDIDOS_CF%' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getLimitecf() As DataTable
        SQL_QUERY = " SELECT tabla, showValue as limite FROM ttipo WHERE tabla like '%MAX_MONTO_CF%' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function setDiferenciaLiquidacion(ByVal valor As String, ByVal id_motivo As String, ByVal tipoMotivo As String) As Integer
        
        SQL_QUERY = "  INSERT INTO [CDIFERENCIA_LIQUIDACION]([idruta],[valor],[moneda],[idMotivo],[tipoMotivo],[idusuario],[fechaEmision]) " _
        & " VALUES (" & id_glo_codRuta & ",'" & valor & "','" & co_glo_moneda & "','" & id_motivo & "','" & tipoMotivo & "','" & id_glo_usuario & "', getDate()) "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function inicializaImportacion() As Integer
        SQL_QUERY = "delete  from [RCLUSTER_SEG]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from cbitacora"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from rrango"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CBODEGA_LIQUIDACION]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CDEPOSITO]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CENCUESTA]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CENCUESTA_DETALLE]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CFACTURA]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CFACTURA_DETALLE]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CINVENTARIO_FISICO]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CLIQUIDACION]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CNOTACREDITO]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CNOTACREDITO_DETALLE]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CRECIBO_DETALLE]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CRECIBO_PAGOS]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [CRECIBO]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from [RCCORRELATIVO]"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RASIGNA_VIAS_PAGO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RSALIDA_INGRESO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RPRODUCTO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RCARGA"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RCARGA_LOG"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from TTIPO WHERE TABLA NOT IN ('WORKFLOW','RAZONES_NO_ATENCION')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RUSUARIO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from RPRESUPUESTO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from DESPACHO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from MOVIMIENTO_INVENTARIO"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "delete  from CDIFERENCIA_LIQUIDACION"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function getListadoCargas() As DataTable
        SQL_QUERY = " select  distinct sd, fechaCreacion+' - '+ substring(HoraCreacion ,0,6) as fechaEmision from rcarga_log  union select 0,'--- Seleccione ---' "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getRutaPorIndice(ByVal idRuta As String) As DataTable
        SQL_QUERY = "SELECT * from RRUTA WHERE id_ruta = " + idRuta
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getRutaActiva() As DataTable
        SQL_QUERY = "SELECT * from RRUTA WHERE esActual = 'true' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getCarga() As DataTable
        SQL_QUERY = _
        "   SELECT idRuta,id_producto, rtrim(descripcion)descripcion,cantidadInicial,cantidadActual, convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja))cj_un,litros " _
        + " FROM ( " _
        + "   	SELECT RC.idRuta,id_producto, descripcion,cantidadActual,cantidadInicial ,cantidadInicial/convert(decimal,unidadesCaja) Resumen, RP.unidadesCaja, RC.cantidadInicial*RP.LitrosUnidad as Litros, ttipo    " _
        + "     FROM " _
        + "     RCARGA_LOG			RC " _
        + "     LEFT JOIN rproducto	RP  " _
        + " ON	RP.id_producto = RC.idProducto	 " _
        + " )QA  " _
        + " WHERE   qa.ttipo = 1 " _
        + " AND     confirmada = false " _
        + " ORDER BY litros desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getCargaPorFechaCarga() As DataTable

        '"   SELECT sd,idRuta,id_producto, rtrim(descripcion)descripcion,cantidadInicial,cantidadActual, " _
        '+ " CASE WHEN unidadesCaja = 1 THEN '0/'+ convert(nvarchar(100),ceiling(floor(resumen))) ELSE convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja)) END AS cj_un, " _
        '+ " litros, confirmada " _
        '+ " FROM ( " _
        '+ "   	SELECT sd,RC.idRuta,id_producto, descripcion,cantidadActual,cantidadInicial ,cantidadInicial/convert(decimal,unidadesCaja) Resumen, RP.unidadesCaja, RC.cantidadInicial*RP.LitrosUnidad as Litros, ttipo, confirmada    " _
        '+ "     FROM " _
        '+ "     RCARGA_LOG			RC " _
        '+ "     LEFT JOIN rproducto	RP  " _
        '+ " ON	RP.id_producto = RC.idProducto	 " _
        '+ " )QA  " _
        '+ " WHERE   qa.ttipo = 1 " _
        '+ " AND     sd = " + sd _
        '+ " ORDER BY id_producto asc "
        SQL_QUERY = _
          " SELECT 	id_producto, rtrim(descripcion)descripcion,cantidadInicial, " _
        & "         CASE WHEN unidadesCaja = 1 THEN '0/'+ convert(nvarchar(100),ceiling(floor(resumen))) ELSE convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja)) END AS cj_un,  " _
        & "         litros, confirmada " _
        & " FROM (  " _
        & " 	SELECT id_producto, descripcion,cantidadInicial ,cantidadInicial/convert(decimal,unidadesCaja) Resumen, RP.unidadesCaja, RC.cantidadInicial*RP.LitrosUnidad as Litros, ttipo, confirmada     " _
        & " 	FROM 	 " _
        & " 	( " _
        & " 		SELECT idproducto, sum(cantidadInicial) CantidadInicial, MAX(CASE WHEN confirmada = 'True' then 1 else 0 end) confirmada " _
        & " 		FROM  RCARGA_LOG RC " _
        & " 		GROUP BY idProducto " _
        & " 	)RC " _
        & " 		LEFT JOIN rproducto	RP " _
        & " 		ON	RP.id_producto = RC.idProducto " _
        & "          )QA   " _
        & " WHERE   qa.ttipo = 1            " _
        & " ORDER BY id_producto asc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getCargaSinConfirmar() As DataTable
        SQL_QUERY = _
        "   SELECT DISTINCT CONFIRMADA FROM RCARGA_LOG WHERE CONFIRMADA = 'FALSE' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getCargaCamion() As DataTable
        SQL_QUERY = _
        " SELECT  " _
        + " idRuta, id_producto, descripcion,ResumenInicial,ResumenActual,unidadesCaja,ttipo,cantidadActual, " _
        + " case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(ResumenInicial))) end as ICJ,  " _
        + " case when unidadesCaja = 1 then           convert(nvarchar(100),ceiling(floor(ResumenInicial))) else convert(nvarchar(100), ceiling ((ResumenInicial - floor(ResumenInicial))*unidadesCaja)) end as  IUN,  " _
        + " case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(ResumenActual)))end as ACJ,  " _
        + " case when unidadesCaja = 1 then       convert(nvarchar(100),ceiling(floor(ResumenActual))) else convert(nvarchar(100), ceiling ((ResumenActual - floor(ResumenActual))*unidadesCaja)) end as AUN  " _
        + " FROM " _
        + " ( " _
        + " 	SELECT  " _
        + " 		RC.idRuta,id_producto, descripcion, " _
        + " 		sum(cantidadInicial)/convert(decimal,avg(unidadesCaja)) ResumenInicial,  " _
        + " 		sum(cantidadActual)/convert(decimal,avg(unidadesCaja)) ResumenActual, " _
        + " 		avg(RP.unidadesCaja) unidadesCaja,    " _
        + " 		ttipo,SUM(cantidadActual)cantidadActual ,categoria     " _
        + " 	FROM  " _
        + " 		RCARGA				RC  " _
        + " 		LEFT JOIN rproducto	RP   " _
        + " 		ON					RP.id_producto = RC.idProducto	  " _
        + "     GROUP BY RC.idRuta,id_producto, descripcion, ttipo ,categoria   " _
        + " )QA " _
        + " order by id_producto "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function eliminarRecargaSinConfirmar() As Integer
        SQL_QUERY = " " _
       + "   DELETE FROM RCARGA_LOG WHERE CONFIRMADA = 'FALSE' "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return 1
    End Function

    Public Function confirmarRuta(ByVal sd As String) As Integer

        '--- Estado de la carga
        SQL_QUERY = " " _
        + "   UPDATE RCARGA_LOG SET confirmada = 'true' " _
        + "   WHERE sd =" + sd
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Combinacion de carga confirmada con carga actual
        SQL_QUERY = " " _
        + " INSERT INTO RCARGA  ([idRuta],[idProducto],[um],[cantidadInicial],[cantidadActual],[control]) " _
        + " SELECT idRuta, idProducto,um,sum(cantidadInicial) cantidadInicial ,sum(cantidadActual) cantidadActual , control " _
        + " FROM " _
        + " ( " _
        + " 	SELECT ID_CARGA,idRuta, idProducto,um,cantidadInicial , cantidadActual, 1 control   FROM RCARGA" _
        + " 	UNION  " _
        + " 	SELECT 0 ID_CARGA,[idRuta],[idProducto],[um],[cantidadInicial],[cantidadActual], 1 [control] FROM RCARGA_LOG  WHERE SD = " + sd _
        + " )QA " _
        + " GROUP BY  idRuta, idProducto,um, control "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Elimina control = 0
        SQL_QUERY = " " _
        + "   DELETE FROM RCARGA WHERE CONTROL = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Actualiza control = 0
        SQL_QUERY = " " _
        + "   UPDATE RCARGA SET CONTROL = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        Return SQL_RESULT


    End Function

    Public Function confirmarOperacionComercial() As Boolean

        '--- Confirmar Factura
        SQL_QUERY = "UPDATE  CFACTURA SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "UPDATE  CFACTURA_DETALLE SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Confirmar Nota de credito
        SQL_QUERY = "UPDATE  cnotaCredito SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "UPDATE  cnotaCredito_detalle SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Confirmar Recibo
        SQL_QUERY = "UPDATE  CRECIBO SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_QUERY = "UPDATE  CRECIBO_detalle SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Confirmar Formas de pago
        SQL_QUERY = "UPDATE  CRECIBO_PAGOS SET estado = 1  WHERE estado = 0"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)



    End Function

    Public Function getCorrelativo() As DataTable
        SQL_QUERY = " SELECT * FROM [RCCORRELATIVO] "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getEnvaseRecibido() As DataTable
        'SQL_QUERY = "" _
        '& " SELECT  id_producto, descripcion, sum(cantidad)cantidadActual, RP.unidadesCaja, NC.ttipo, NC.idRuta, 0 as cantidadInicial, sum(litrosUnidad) litrosVendido  " _
        '& "  FROM " _
        '& "      [CNOTACREDITO] NC LEFT JOIN  " _
        '& "      [CNOTACREDITO_DETALLE]	 RC  " _
        '& "      on id_EncNC = idEncNC  " _
        '& "  LEFT JOIN rproducto	RP   " _
        '& "  ON		RP.id_producto = RC.idProducto	  " _
        '& "  WHERE 	nc.estado = 1 AND id_producto is not null " _
        '& "  GROUP BY 	id_producto, descripcion,RP.unidadesCaja, NC.ttipo,NC.idRuta           " _
        '& "  order by ttipo, descripcion "

        SQL_QUERY = "" _
        & " SELECT  COALESCE(id_producto,0) as id_producto, COALESCE(descripcion,'N/A') as descripcion, sum(cantidad)cantidadActual, COALESCE(RP.unidadesCaja,0) as unidadesCaja, NC.ttipo, NC.idRuta, 0 as cantidadInicial, COALESCE(sum(litrosUnidad),0) AS litrosVendido " _
        & "  FROM " _
        & "      [CNOTACREDITO] NC LEFT JOIN  " _
        & "      [CNOTACREDITO_DETALLE]	 RC  " _
        & "      on id_EncNC = idEncNC  " _
        & "  LEFT JOIN rproducto	RP   " _
        & "  ON		RP.id_producto = RC.idProducto	  " _
        & "  WHERE 	nc.estado = 1	  " _
        & "  GROUP BY 	id_producto, descripcion,RP.unidadesCaja, NC.ttipo,NC.idRuta           " _
        & "  order by ttipo, descripcion "

        '" SELECT  " _
        '+ " *, " _
        '+ "  case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(Resumen)))end as ACJ,  " _
        '+ " case when unidadesCaja = 1 then       convert(nvarchar(100),ceiling(floor(Resumen))) else convert(nvarchar(100), ceiling ((Resumen - floor(Resumen))*unidadesCaja)) end as AUN  " _
        '+ " FROM " _
        '+ " ( " _
        '+ "	SELECT 		 " _
        '+ " 	id_producto, descripcion," _
        '+ "		sum(cantidad)cantidad, " _
        '+ " 	sum(cantidad)/convert(decimal,unidadesCaja) Resumen, 		 " _
        '+ "		RP.unidadesCaja,  " _
        '+ "     NC.ttipo  ,  " _
        '+ "     NC.idRuta " _
        '+ " FROM" _
        '+ "     [CNOTACREDITO] NC LEFT JOIN " _
        '+ "     [CNOTACREDITO_DETALLE]	 RC " _
        '+ "     on id_EncNC = idEncNC " _
        '+ " LEFT JOIN rproducto	RP  " _
        '+ " ON					RP.id_producto = RC.idProducto	 " _
        '+ " WHERE nc.estado = 1" _
        '+ " GROUP BY id_producto, descripcion,RP.unidadesCaja, NC.ttipo,NC.idRuta " _
        '+ " )QA " _
        '+ " order by ttipo, descripcion "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getResumenMarcas() As DataTable
        SQL_QUERY = " select  rp.idMarca, tt.showValue desc_marca, sum(rp.litrosUnidad * cf.cantidad) Total_litros, sum(cf.importeSinIva) importeSinIva " _
                    + "  from cfactura_detalle cf, rproducto rp, ttipo tt, cfactura ft " _
                    + "  where cf.idProducto = rp.id_producto  " _
                    + "  and tt.tabla = rp.idMarca  " _
                    + "  and ft.id_encFactura = cf.idEncFactura " _
                    + "  and cf.idRubro = 'L' " _
                    + "  and ft.estado = 1 " _
                    + "  GROUP BY rp.idMarca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getResumenSINFEL() As DataTable
        SQL_QUERY = " SELECT fNumero numero, idCliente, importe, 'FACTURA' tipo FROM CFACTURA " _
                    + "  WHERE ESTADO = 1  AND preimpreso is null " _
                    + "  UNION ALL   " _
                    + "  SELECT ncNumero numero, idCliente, importe, 'NOTA CREDITO' tipo FROM CNOTACREDITO " _
                    + "  WHERE ESTADO = 1 AND preimpreso is null "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getMovimientos() As DataTable
        SQL_QUERY = " select sum(km_inicio) km_inicio, sum(km_fin) km_fin, MIN(hora) hora_salida, MAX(hora) hora_entrada " _
                    + "  from( select respuesta km_inicio, 0 km_fin, case when null  is null then null else respuesta end hora " _
                    + "  from rsalida_ingreso where pregunta like '%KILOMETRAJE INICIAL%' " _
                    + "  UNION ALL  " _
                    + "  select 0 km_inicio, case when respuesta  is null then 0 else respuesta end km_fin, case when null  is null then null else respuesta end hora " _
                    + "  from rsalida_ingreso " _
                    + "  where pregunta = 'KILOMETRAJE FINAL' " _
                    + "  UNION ALL " _
                    + "  select 0 km_inicio, 0 km_fin, respuesta hora " _
                    + "  from rsalida_ingreso " _
                    + "  where pregunta = 'HORA DE SALIDA' " _
                    + "  UNION ALL " _
                    + "  select 0 km_inicio, 0 km_fin,  case when respuesta  is null then null else respuesta end hora " _
                    + "  from rsalida_ingreso " _
                    + "  where pregunta = 'HORA ENTRADA ?' " _
                    + "  )d "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getMovimientosCobros() As DataTable
        SQL_QUERY = " SELECT SUM(TOTAL) TOTAL, SUM(C) NCOBRADOS, SUM(NC) COBRADOS " _
                    + "  FROM ( SELECT COUNT(*) total, 0 c, 0 nc " _
                    + "  FROM RCXC " _
                    + "  UNION ALL  " _
                    + "  SELECT 0 total, COUNT(*) NC, 0 C " _
                    + "  FROM RCXC " _
                    + "  WHERE PAGADO is null " _
                    + "  UNION ALL " _
                    + "  select 0 total, 0 NC, COUNT(*)  C " _
                    + "  from RCXC " _
                    + "  WHERE PAGADO is not null " _
                    + "  ) COB "
                    
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getEficiencia() As DataTable
        SQL_QUERY = " select sum(despachado) despachos,  sum(total)  total " _
                    + "  from ( select count(*) total, 0 despachado " _
                    + "  from despacho " _
                    + "  UNION ALL  " _
                    + "  select 0 total, count(*) despachado " _
                    + "  from despacho " _
                    + "  where despachado is not null " _
                    + "  ) des " 

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getResumenPresupuesto() As DataTable
        SQL_QUERY = " select 	pr.marca, tt.showvalue descripcion,sum(pr.lit_presupuesto)  lit_presupuesto, sum(pr.mt_presupuesto)  mt_presupuesto,  " _
                    + "  sum(pr.lit_real)  lit_real, sum(pr.venta_real)  venta_real, sum(pr.nec_lit_dia)  nec_lit_dia,  " _
                    + "  sum(pr.nec_vta_dia)  nec_vta_dia, sum(pr.nec_lit_mes)  nec_lit_mes, sum(pr.nec_vta_mes)  nec_vta_mes " _
                    + "  from rpresupuesto_cliente pr, ttipo tt " _
                    + "  where pr.marca = tt.tabla " _
                    + "  GROUP BY pr.marca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getResumenPresupuestoDias(ByVal diad As String) As DataTable
        SQL_QUERY = " select 	pr.marca, tt.showvalue descripcion,sum(pr.lit_presupuesto)  lit_presupuesto, sum(pr.mt_presupuesto)  mt_presupuesto,  " _
                    + "  sum(pr.lit_real)  lit_real, sum(pr.venta_real)  venta_real, sum(pr.nec_lit_dia)  nec_lit_dia,  " _
                    + "  sum(pr.nec_vta_dia)  nec_vta_dia, sum(pr.nec_lit_mes)  nec_lit_mes, sum(pr.nec_vta_mes)  nec_vta_mes " _
                    + "  from rpresupuesto_cliente pr, ttipo tt, rcliente cl " _
                    + "  where pr.marca = tt.tabla " _
                    + "  and pr.idcliente = cl.id_cliente " _
                    + "  and cl.diaVisita =  '" + diad + "' " _
                    + "  GROUP BY pr.marca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getPresupuesto() As DataTable
        SQL_QUERY = " select pr.marca codigo, tt.showvalue descripcion,sum(pr.lit_presupuesto)  lit_presupuesto, sum(pr.mt_presupuesto)  mt_presupuesto,  " _
                    + "  sum(pr.lit_real)  lit_real, sum(pr.venta_real)  venta_real, sum(pr.nec_lit_dia)  nec_lit_dia,  " _
                    + "  sum(pr.nec_vta_dia)  nec_vta_dia, sum(pr.nec_lit_mes)  nec_lit_mes, sum(pr.nec_vta_mes)  nec_vta_mes " _
                    + "  from rpresupuesto_cliente pr, ttipo tt " _
                    + "  where pr.marca = tt.tabla " _
                    + "  GROUP BY pr.marca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPresupuestoDia(ByVal dia As String) As DataTable
        SQL_QUERY = " select 	pr.marca codigo, tt.showvalue descripcion,sum(pr.lit_presupuesto)  lit_presupuesto, sum(pr.mt_presupuesto)  mt_presupuesto,  " _
                    + "  sum(pr.lit_real)  lit_real, sum(pr.venta_real)  venta_real, sum(pr.nec_lit_dia)  nec_lit_dia,  " _
                    + "  sum(pr.nec_vta_dia)  nec_vta_dia, sum(pr.nec_lit_mes)  nec_lit_mes, sum(pr.nec_vta_mes)  nec_vta_mes " _
                    + "  from rpresupuesto_cliente pr, ttipo tt, rcliente cl " _
                    + "  where pr.marca = tt.tabla " _
                    + "  and pr.idcliente = cl.id_cliente " _
                    + "  and cl.diaVisita =  '" + dia + "' " _
                    + "  GROUP BY pr.marca, tt.showValue, cl.diaVisita "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPresupuestoCliente(ByVal idcliente As String) As DataTable
        SQL_QUERY = " select pr.marca codigo , tt.showvalue descripcion ,sum(pr.lit_presupuesto)  lit_presupuesto, sum(pr.mt_presupuesto)  mt_presupuesto,  " _
                    + "  sum(pr.lit_real)  lit_real, sum(pr.venta_real)  venta_real, sum(pr.nec_lit_dia)  nec_lit_dia,  " _
                    + "  sum(pr.nec_vta_dia)  nec_vta_dia, sum(pr.nec_lit_mes)  nec_lit_mes, sum(pr.nec_vta_mes)  nec_vta_mes " _
                    + "  from rpresupuesto_cliente pr, ttipo tt " _
                    + "  where pr.marca = tt.tabla " _
                    + "  AND pr.idcliente =  " + idcliente + " " _
                    + "  GROUP BY pr.marca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getDocumentosEmitidos() As DataTable
        SQL_QUERY = " " _
         '+ "  SELECT DISTINCT * FROM     " _
        '         + "          (    " _
        '        + "          SELECT	CASE a.ttipo when 'ODV' THEN 'FACTURA' WHEN 'ZTAE' THEN 'FACTURA POR FALTANTES' WHEN 'ZTAP' THEN 'FACTURA POR FALTANTES' END tipoD,       " _
        '       + "          			a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe , 'CONTADO'  CPAGO , '0' FRELSE,'0' FRELNO,a.ESTADO,a.IDRUTA, CAST(sum (b.cantidad * pr.litrosUnidad) AS REAL) litros " _
        '      + "          FROM 		cfactura a left join cfactura_detalle b on a.id_encFactura = b.idEncFactura inner join rproducto pr on b.idProducto = pr.id_producto " _
        '     + "          WHERE 	a.fechaVence is null    and b.idRubro = 'L'  " _
        '    + "          GROUP BY a.ttipo, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe, a.ESTADO,a.IDRUTA " _
        '   + "          UNION ALL " _
        '         + "          SELECT 	CASE a.ttipo when 'ODV' THEN 'FACTURA' 	WHEN 'ZTAE' THEN 'FACTURA POR FALTANTES' WHEN 'ZTAP' THEN 'FACTURA POR FALTANTES' END tipoD,      " _
        '        + "          			a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe,  CASE  when a.fechaemision = a.fechavence THEN 'CONTADO' ELSE 'CREDITO'  END AS TIPO, '0' fserie,'0' fnumero,a.ESTADO,a.IDRUTA, CAST(sum (b.cantidad * pr.litrosUnidad) AS REAL) litros " _
        '       + "          FROM 		cfactura   a left join cfactura_detalle b on a.id_encFactura = b.idEncFactura inner join rproducto pr on b.idProducto = pr.id_producto " _
        '      + "          WHERE 	a.fechaVence is not null   and b.idRubro = 'L'   " _
        '     + "          GROUP BY a.ttipo, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe, a.ESTADO,a.IDRUTA,a.fechavence " _
        '    + "          )F " _
        '   + "          UNION    " _
        '         + "          SELECT DISTINCT	'NC ' + T.showValue tipoD,C.ncserie,C.ncnumero,c.fechaemision,c.idcliente,abs(c.importe), 'NC'  TIPO, F.fserie ,  CONVERT(NVARCHAR,F.fnumero)fnumero, C.ESTADO,C.IDRUTA, 0 litros " _
        '        + "          FROM				cnotaCredito C  LEFT JOIN TTIPO T ON C.ttipo=T.dataValue     " _
        '       + "          					left join 	cfactura F on C.idencfactura = F.id_encfactura LEFT JOIN cfactura_detalle b on  f.id_encFactura = b.idEncFactura " _
        '      + "          WHERE				T.tabla = 'NOTA_CREDITO'    " _
        '     + "          GROUP BY T.showValue, C.ncserie, C.ncnumero, C.fechaemision, C.idCliente, C.importe, F.fserie, F.fnumero, C.estado, C.idruta  " _
        '    + "          UNION " _
        '         + " 	SELECT DISTINCT * FROM   " _
        '        + " 	(   " _
        '       + " 		SELECT   	'DESCUENTO' AS tipoD, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,abs(a.importeDesto + a.importeDestopp) importeDesto, 'APLICADO'  CPAGO , '0' FRELSE,'0' FRELNO,a.ESTADO,a.IDRUTA, 0 litros " _
        '      + " 		FROM 	cfactura a left join cfactura_detalle b  on a.id_encFactura = b.idEncFactura " _
        '     + " 		WHERE 	a.fechaVence is null " _
        '    + " 		and  a.importeDesto + a.importeDestoPP <> 0 " _
        '   + "        GROUP BY a.fserie, a.fnumero, a.fechaemision, a.idcliente, a.importeDesto, a.importeDestopp, a.estado, a.idruta " _
        '  + " 		UNION    " _
        ' + " 		SELECT   	'DESCUENTO' AS tipoD, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,abs(a.importeDesto + a.importeDestopp) importeDesto,'PP'  TIPO , '0' fserie,'0' fnumero,a.ESTADO,a.IDRUTA , 0 litros   " _
        '+ " 		FROM	cfactura a left join cfactura_detalle b  on a.id_encFactura = b.idEncFactura   " _
        '         + " 		WHERE 	a.fechaVence is not null     " _
        '        + " 		and  a.importeDesto + a.importeDestoPP <> 0     " _
        '       + "        GROUP BY a.fserie, a.fnumero, a.fechaEmision, a.idcliente, a.importeDesto, a.importeDestopp, a.estado, a.idruta " _
        '      + " 	)D   " _
        '     + " 	UNION    " _
        '    + " 	SELECT DISTINCT * FROM    " _
        '   + "           (   " _
        '  + "          	 SELECT 'RECIBO' TIPOD , r.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, F.fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros " _
        '         + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
        '        + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
        '       + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago     " _
        '      + "          	 left join cfactura F on R. idencfactura = F.id_encfactura " _
        '     + "             left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura " _
        '    + "          	 WHERE D.IMPORTE >0  and F.fserie is not null  and p.idViapago <>'CR'   AND p.idViaPago NOT  IN ('W','Y','Z')    " _
        '   + "             GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta " _
        '         + "             UNION " _
        '        + "             SELECT 'CUPON_SORPRESA' TIPOD , r.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, F.fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros " _
        '       + "             FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo  " _
        '      + "             LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo               	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago left join cfactura F on R. idencfactura = F.id_encfactura left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura   " _
        '     + "             WHERE D.IMPORTE >0  and F.fserie is not null  and p.idViapago <>'CR'  AND p.idViaPago  IN ('W') " _
        '    + "             GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta  " _
        '  + "             UNION  " _
        '   + "             SELECT 'CUPON2' TIPOD , r.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, F.fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros  " _
        '+ "             FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo  " _
        '         + "             LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo  LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago left join cfactura F on R. idencfactura = F.id_encfactura left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura  " _
        '        + "             WHERE D.IMPORTE >0  and F.fserie is not null  and p.idViapago <>'CR'  AND p.idViaPago  IN ('Y') " _
        '       + "             GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta   " _
        '      + "             UNION  " _
        '     + "             SELECT 'CUPON5' TIPOD , r.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, F.fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros  " _
        '    + "             FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo " _
        '   + "              LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  left join cfactura F on R. idencfactura = F.id_encfactura  left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura  " _
        '  + "              WHERE D.IMPORTE >0  and F.fserie is not null  and p.idViapago <>'CR'  AND p.idViaPago  IN ('Z') " _
        ' + "              GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta" _
        '         + "          	 union all     " _
        '        + "          	 SELECT 'RECIBO' TIPOD , RSERIE, R.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, cxc.serie, cxc.numero , R.ESTADO,R.IDRUTA, 0 litros " _
        '       + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
        '      + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
        '     + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  " _
        '    + "          	 left join rcxc cxc on R.idEncCxc = id_cxc     " _
        '   + "          	 WHERE D.IMPORTE >0  and p.idViapago <>'CR'   AND p.idViaPago NOT  IN ('W','Y','Z')  and cxc.numero is not null " _
        '  + "          	 UNION " _
        ' + "          	 SELECT 'CUPON_SORPRESA' TIPOD , RSERIE, R.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, cxc.serie, cxc.numero , R.ESTADO,R.IDRUTA, 0 litros " _
        '         + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
        '        + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
        '       + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  " _
        '      + "          	 left join rcxc cxc on R.idEncCxc = id_cxc     " _
        '     + "          	 WHERE D.IMPORTE >0  and p.idViapago <>'CR' AND p.idViaPago = 'W' and cxc.numero is not null " _
        '    + "          	 UNION " _
        '   + "          	 SELECT 'CUPON2' TIPOD , RSERIE, R.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, cxc.serie, cxc.numero , R.ESTADO,R.IDRUTA, 0 litros " _
        '  + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
        ' + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
        '         + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  " _
        '        + "          	 left join rcxc cxc on R.idEncCxc = id_cxc     " _
        '       + "          	 WHERE D.IMPORTE >0  and p.idViapago <>'CR' AND p.idViaPago = 'Y' and cxc.numero is not null " _
        '      + "          	 UNION " _
        '     + "          	 SELECT 'CUPON5' TIPOD , RSERIE, R.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, cxc.serie, cxc.numero , R.ESTADO,R.IDRUTA, 0 litros " _
        '    + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
        '   + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
        '  + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  " _
        ' + "          	 left join rcxc cxc on R.idEncCxc = id_cxc     " _
        '+ "          	 WHERE D.IMPORTE >0  and p.idViapago <>'CR'  AND p.idViaPago = 'Z' and cxc.numero is not null " _
        '         + " UNION ALL  " _
        '        + " SELECT 'CREDITOS CONCEDIDOS' TIPOD , R.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros" _
        '       + " FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo " _
        '      + " LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo " _
        '     + " LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago " _
        '    + " left join cfactura f on R.idencfactura = f.id_encfactura " _
        '   + " left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura " _
        '         + " WHERE  p.idViapago = 'CR' " _
        '        + " GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta " _
        '       + "          )R   " _
        '      + "        ORDER BY tipoD desc  "


        'Se agrego el segundo UNION ALL para que aparezcan los documentos que no tienen detalle.
        SQL_QUERY = " " _
         + "  SELECT DISTINCT * FROM     " _
         + "          (    " _
         + "          SELECT	CASE a.ttipo when 'ODV' THEN 'FACTURA' WHEN 'ZTAE' THEN 'FACTURA POR FALTANTES' WHEN 'ZTAP' THEN 'FACTURA POR FALTANTES' WHEN 'CD' THEN 'CAMBIOS'  END tipoD,       " _
         + "          			a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe , 'CONTADO'  CPAGO , '0' FRELSE,'0' FRELNO,a.ESTADO,a.IDRUTA, CASE a.Estado when 1 THEN CAST(sum (b.cantidad * pr.litrosUnidad) AS REAL) ELSE 0 END litros " _
         + "          FROM 		cfactura a left join cfactura_detalle b on a.id_encFactura = b.idEncFactura inner join rproducto pr on b.idProducto = pr.id_producto " _
         + "          WHERE 	a.fechaVence is null    and b.idRubro = 'L'  " _
         + "          GROUP BY a.ttipo, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe, a.ESTADO,a.IDRUTA " _
         + "          UNION ALL " _
         + "          SELECT	CASE a.ttipo when 'ODV' THEN 'FACTURA' WHEN 'ZTAE' THEN 'FACTURA POR FALTANTES' WHEN 'ZTAP' THEN 'FACTURA POR FALTANTES' WHEN 'CD' THEN 'CAMBIOS'  END tipoD,       " _
         + "           			a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe , 'CONTADO'  CPAGO , '0' FRELSE,'0' FRELNO,a.ESTADO,a.IDRUTA, CASE a.Estado when 1 THEN CAST(sum (0) AS REAL) ELSE 0 END litros  " _
         + "           FROM 		cfactura a left join cfactura_detalle b on a.id_encFactura = b.idEncFactura  " _
         + "           WHERE 	a.fechaVence is null    and a.estado = 2 " _
         + "           GROUP BY a.ttipo, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe, a.ESTADO,a.IDRUTA " _
         + "          UNION ALL " _
         + "          SELECT 	CASE a.ttipo when 'ODV' THEN 'FACTURA' 	WHEN 'ZTAE' THEN 'FACTURA POR FALTANTES' WHEN 'ZTAP' THEN 'FACTURA POR FALTANTES' WHEN 'CD' THEN 'CAMBIOS' END tipoD,      " _
         + "          			a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe,  CASE  when a.fechaemision = a.fechavence THEN 'CONTADO' ELSE 'CREDITO'  END AS TIPO, '0' fserie,'0' fnumero,a.ESTADO,a.IDRUTA, CASE a.Estado when 1 THEN CAST(sum (b.cantidad * pr.litrosUnidad) AS REAL) ELSE 0 END litros " _
         + "          FROM 		cfactura   a left join cfactura_detalle b on a.id_encFactura = b.idEncFactura inner join rproducto pr on b.idProducto = pr.id_producto " _
         + "          WHERE 	a.fechaVence is not null   and b.idRubro = 'L'   " _
         + "          GROUP BY a.ttipo, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,a.importe, a.ESTADO,a.IDRUTA,a.fechavence " _
         + "          )F " _
         + "          UNION    " _
         + "          SELECT DISTINCT	'NC ' + T.showValue tipoD,C.ncserie,C.ncnumero,c.fechaemision,c.idcliente,abs(c.importe), 'NC'  TIPO, F.fserie ,  CONVERT(NVARCHAR,F.fnumero)fnumero, C.ESTADO,C.IDRUTA, 0 litros " _
         + "          FROM				cnotaCredito C  LEFT JOIN TTIPO T ON C.ttipo=T.dataValue     " _
         + "          					left join 	cfactura F on C.idencfactura = F.id_encfactura LEFT JOIN cfactura_detalle b on  f.id_encFactura = b.idEncFactura " _
         + "          WHERE				T.tabla = 'NOTA_CREDITO'    " _
         + "          GROUP BY T.showValue, C.ncserie, C.ncnumero, C.fechaemision, C.idCliente, C.importe, F.fserie, F.fnumero, C.estado, C.idruta  " _
         + "          UNION " _
         + " 	SELECT DISTINCT * FROM   " _
         + " 	(   " _
         + " 		SELECT   	'DESCUENTO' AS tipoD, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,abs(a.importeDesto + a.importeDestopp) importeDesto, 'APLICADO'  CPAGO , '0' FRELSE,'0' FRELNO,a.ESTADO,a.IDRUTA, 0 litros " _
         + " 		FROM 	cfactura a left join cfactura_detalle b  on a.id_encFactura = b.idEncFactura " _
         + " 		WHERE 	a.fechaVence is null " _
         + " 		and  a.importeDesto + a.importeDestoPP <> 0 " _
         + "        GROUP BY a.fserie, a.fnumero, a.fechaemision, a.idcliente, a.importeDesto, a.importeDestopp, a.estado, a.idruta " _
         + " 		UNION    " _
         + " 		SELECT   	'DESCUENTO' AS tipoD, a.fserie,a.fnumero,a.fechaEmision,a.idcliente,abs(a.importeDesto + a.importeDestopp) importeDesto,'PP'  TIPO , '0' fserie,'0' fnumero,a.ESTADO,a.IDRUTA , 0 litros   " _
         + " 		FROM	cfactura a left join cfactura_detalle b  on a.id_encFactura = b.idEncFactura   " _
         + " 		WHERE 	a.fechaVence is not null     " _
         + " 		and  a.importeDesto + a.importeDestoPP <> 0     " _
         + "        GROUP BY a.fserie, a.fnumero, a.fechaEmision, a.idcliente, a.importeDesto, a.importeDestopp, a.estado, a.idruta " _
         + " 	)D   " _
         + " 	UNION    " _
         + " 	SELECT DISTINCT * FROM    " _
         + "           (   " _
         + "          	 SELECT 'RECIBO' TIPOD , r.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, F.fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros " _
         + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
         + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
         + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago     " _
         + "          	 left join cfactura F on R. idencfactura = F.id_encfactura " _
         + "             left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura " _
         + "          	 WHERE D.IMPORTE >0  and F.fserie is not null  and p.idViapago <>'CR'      " _
         + "             GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta " _
         + "          	 union all     " _
         + "          	 SELECT 'RECIBO' TIPOD , RSERIE, R.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, cxc.serie, cxc.numero , R.ESTADO,R.IDRUTA, 0 litros " _
         + "          	 FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo      " _
         + "          	 LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo     " _
         + "          	 LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago  " _
         + "          	 left join rcxc cxc on R.idEncCxc = id_cxc     " _
         + "          	 WHERE D.IMPORTE >0  and p.idViapago <>'CR'    and cxc.numero is not null " _
         + " UNION ALL  " _
         + " SELECT 'CREDITOS CONCEDIDOS' TIPOD , R.RSERIE, r.rNUMERO,r.fechaEmision, r.idcliente,P.IMPORTE, V.descripcion, fserie, CONVERT(NVARCHAR, F.fnumero)fnumero , R.ESTADO,R.IDRUTA, 0 litros" _
         + " FROM  CRECIBO R Left join CRECIBO_DETALLE D  on D.idencrecibo = id_encrecibo " _
         + " LEFT JOIN CRECIBO_PAGOS P on P.idencrecibo = id_encrecibo " _
         + " LEFT JOIN RVIAS_PAGO V ON V.VIA = P.idviapago " _
         + " left join cfactura f on R.idencfactura = f.id_encfactura " _
         + " left join cfactura_detalle b  on F.id_encFactura = b.idEncFactura " _
         + " WHERE  p.idViapago = 'CR' " _
         + " GROUP BY R.rserie, R.rnumero, R.fechaEmision, R.idCliente, P.importe, V.descripcion, F.fserie, F.fnumero, R.estado, R.idruta " _
         + "          )R   " _
         + "        ORDER BY tipoD desc  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getChequesRecibidos() As DataTable
        SQL_QUERY = _
        "   SELECT 		documento, showvalue,P.importe,idRuta " _
        + " FROM 		CRECIBO_PAGOS P " _
        + " LEFT JOIN 	ttipo on CONVERT (NCHAR(30),idinstitucion) = datavalue  " _
        + " LEFT JOIN 	CRECIBO R on idEncRecibo = id_encrecibo " _
        + " WHERE TABLA = 'BANCOS' AND IDVIAPAGO = 'C' " _
        + " and R.estado  = 1  " _
        + " ORDER BY importe DESC "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getIntegracionRep() As DataTable
        SQL_QUERY = "" _
        + " select vt.partida, vt.idRubro, (vt.importe + ds.descuento) importe, vt.integracion from (  " _
        + " select 	 '(+) VENTAS ' Partida ,'' idRubro, sum (D.importe) importe, 'integracion' integracion  " _
        + " from 	[cfactura] E  " _
        + "  		left join [CFACTURA_DETALLE] D   " _
        + "  		on  idEncfactura = id_encfactura " _
        + " where 	E.estado = 1 ) vt left join (select sum ( E.importeDesto) as descuento from cfactura e where e.estado = 1 ) ds on 1 = 1 " _
        + " union all   " _
        + " select 	'     Liquido' Partida ,'' idRubro, sum (D.importe) importe , 'otros' integracion  " _
        + " from 	[cfactura] E  " _
        + "  		left join [CFACTURA_DETALLE] D   " _
        + "  		on  idEncfactura = id_encfactura " _
        + "  where 	E.estado = 1 and idRubro = 'L'   " _
        + "  union all    " _
        + "  select 	'     Envase' Partida ,'' idRubro, sum (D.importe) importe, 'otros' integracion   " _
        + " from 	[cfactura] E  " _
        + " 		left join [CFACTURA_DETALLE] D   " _
        + "         on E.id_encFactura = idEncFactura " _
        + " where 	E.estado = 1 and idRubro <> 'L'    " _
        + " union all   " _
        + " select 	'(-) ENVASE RECIBIDO' Partida,'' idRubro, sum(importe*-1)importe, 'integracion' integracion  " _
        + " from 	[CNOTACREDITO]   " _
        + " where 	estado = 1  and ttipo = 4 and idEncFactura <> 0   " _
        + " union all    " _
        + " select 	'(-) CREDITOS CONCEDIDOS' Partida, ''idRubro,sum(D.importe)importe, 'integracion' integracion  " _
        + " from 	[CRECIBO] E " _
        + "  		left join [CRECIBO_PAGOS]    D " _
        + "  on 		id_encRecibo = idencrecibo " _
        + " where 	E.Estado = 1 and idviapago = 'CR'   " _
        + " union all   " _
        + " select 	'(-) DESCUENTOS APLICADOS' Partida, ''idRubro, sum(importeDesto*-1) importeDesto , 'otros' integracion  " _
        + "  from 	crecibo   where 	Estado = 1" _
        + "  union all   " _
        + " select 	'(+) CREDITOS COBRADOS', ''idRubro,sum (P.importe) importe, 'integracion' integracion        " _
        + " from     	crecibo R             left join crecibo_pagos P  on id_encRecibo =  idEncRecibo " _
        + " where 	R.dotipo = 'CXC'  AND R.Estado = 1 " _
        + " union all " _
        + " select '(-) OTRAS VIAS' Partida, ''idRubro, sum(cp.importe*-1) importe, 'integracion' integracion " _
        + " from crecibo_pagos cp, rvias_pago rv, crecibo rc " _
        + " where(cp.idViaPago = rv.via) " _
        + " and rc.id_encRecibo = cp.idEncRecibo " _
        + " and rc.estado = 1 " _
        + " and rv.via in (select via from rvias_pago pg where pg.simbolo = 2) " _
        + " union all " _
        + "  select 	'TOTAL_DEPOSITO' Partida,'' idRubro, sum(p.importe) importe, 'integracion' integracion  " _
        + "  from 	crecibo R left join crecibo_pagos P on id_EncRecibo = idEncRecibo  " _
        + "  WHERE 	IDVIAPAGO<>'CR'  and R.Estado = 1 " _
        + " union all " _
        + " select 	'DEVOLUCION PT' Partida,'' idRubro, sum(importe*-1)importe , 'otros' integracion   " _
        + " from [CBODEGA_LIQUIDACION]            " _
        + " where ttipo = 1   "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT


        '+ " SELECT ap.partida, ap.idRubro, (sum(ap.importe) + sum(ap.importe2)) importe, 'integracion' integracion  FROM  ( " _
        '+ "  select 	'TOTAL_DEPOSITO' Partida,'' idRubro, sum(p.importe) importe, 'integracion' integracion  " _
        '        + "  from 	crecibo R left join crecibo_pagos P on id_EncRecibo = idEncRecibo  " _
        '       + "  WHERE 	IDVIAPAGO<>'CR'  and R.Estado = 1 " _
        '      + "  UNION ALL " _
        '     + "  select 'TOTAL_DEPOSITO' Partida, ''idRubro, 0 importe, sum(cp.importe*-1) importe2, 'integracion' integracion  " _
        '    + "  from crecibo_pagos cp, rvias_pago rv, crecibo rc  " _
        '   + "  where(cp.idViaPago = rv.via) " _
        '  + "  and rc.id_encRecibo = cp.idEncRecibo " _
        ' + "  and rc.estado = 1 " _
        '        + "  and rv.via in (select via from rvias_pago pg where pg.simbolo = 2) " _
        '       + "  ) ap  GROUP BY ap.partida, ap.idRubro " _
        '      + " union all " _

        '+ " select 	'TOTAL_DEPOSITO' Partida,'' idRubro, sum(p.importe) importe  , 'integracion' integracion       " _
        '+ " from 	crecibo R left join crecibo_pagos P on id_EncRecibo = idEncRecibo   " _
        '+ " WHERE 	IDVIAPAGO<>'CR'  and R.Estado = 1 " _
    End Function


    Public Function getIntegracion() As DataTable
        SQL_QUERY = "" _
        + " select 	 '(+) VENTAS' Partida ,'' idRubro, sum (D.importe) importe    " _
        + " from 	[cfactura] E    " _
        + "           		left join [CFACTURA_DETALLE] D     " _
        + "           		on  idEncfactura = id_encfactura   " _
        + "          where 	E.estado = 1  " _
        + " UNION ALL  " _
        + " select 	 '          Contado ' Partida ,'' idRubro, sum (P.importe) importe     " _
        + "          from 	[cfactura] E     " _
        + "                    		left join [CRECIBO] D      " _
        + "                    		on  idEncfactura = id_encfactura    " _
        + "                    			left join [CRECIBO_PAGOS] P " _
        + "                    			on P.idEncRecibo = id_encRecibo " _
        + " where 	E.estado = 1 AND P.idViaPago <> 'CR' " _
        + " union all " _
        + " select 	 '          Credito ' Partida ,'' idRubro, sum (P.importe) importe     " _
        + "          from 	[cfactura] E     " _
        + "                    		left join [CRECIBO] D      " _
        + "                    		on  idEncfactura = id_encfactura    " _
        + "                    			left join [CRECIBO_PAGOS] P " _
        + "                    			on P.idEncRecibo = id_encRecibo " _
        + " where 	E.estado = 1 AND P.idViaPago = 'CR' " _
          + " UNION ALL  " _
        + " select 	'          Envase ' Partida,'' idRubro, sum(importe)importe   " _
        + " from 	[CNOTACREDITO]    " _
        + " where 	estado = 1  and ttipo = 4    and idEncFactura <> 0 " _
        + " UNION ALL   " _
        + " select 	 'DE LOS CUALES' Partida ,'' idRubro, 0  " _
        + " union all   " _
        + " select 	'     Liquido' Partida ,'' idRubro, sum (D.importe) importe   " _
        + " from 	[cfactura] E  " _
        + "  		left join [CFACTURA_DETALLE] D   " _
        + "  		on  idEncfactura = id_encfactura " _
        + "  where 	E.estado = 1 and idRubro = 'L'   " _
        + "  union all    " _
        + "  select 	'     Envase' Partida ,'' idRubro, sum (D.importe) importe   " _
        + " from 	[cfactura] E  " _
        + " 		left join [CFACTURA_DETALLE] D   " _
        + "         on E.id_encFactura = idEncFactura " _
        + " where 	E.estado = 1 and idRubro <> 'L'    " _
        + " union all   " _
        + " select 	'(-) ENVASE RECIBIDO' Partida,'' idRubro, sum(importe*-1)importe   " _
        + " from 	[CNOTACREDITO]    " _
        + " where 	estado = 1  and ttipo = 4    " _
        + " UNION ALL  " _
        + " select 	'          Envase recibido en ventas' Partida,'' idRubro, sum(importe*-1)importe   " _
        + " from 	[CNOTACREDITO]    " _
        + " where 	estado = 1  and ttipo = 4    and idEncFactura <> 0 " _
        + " UNION ALL  " _
        + " select 	'          Envase recibido en cobros' Partida,'' idRubro, sum(importe*-1)importe   " _
        + " from 	[CNOTACREDITO]    " _
        + " where 	estado = 1  and ttipo = 4   and idEncFactura = 0 " _
        + " union all    " _
        + " select 	'(+) CREDITOS CONCEDIDOS' Partida, ''idRubro,sum(D.importe)importe  " _
        + " from 	[CRECIBO] E " _
        + "  		left join [CRECIBO_PAGOS]    D " _
        + "  on 		id_encRecibo = idencrecibo " _
        + " where 	E.Estado = 1 and idviapago = 'CR'   " _
        + " union all   " _
        + " select 	'(-) DESCUENTOS APLICADOS' Partida, ''idRubro, sum(importeDesto*-1) importeDesto   " _
        + "  from 	crecibo   where 	Estado = 1" _
        + "  union all   " _
        + " select 	'(+) CREDITOS COBRADOS', ''idRubro,sum (P.importe) importe        " _
        + " from     	crecibo R             left join crecibo_pagos P  on id_encRecibo =  idEncRecibo " _
        + " where 	R.dotipo = 'CXC'  AND R.Estado = 1 " _
        + " union all " _
        + " select '(-) OTRAS VIAS' Partida, ''idRubro, sum(cp.importe*-1) importe " _
        + " from crecibo_pagos cp, rvias_pago rv, crecibo rc " _
        + " where(cp.idViaPago = rv.via) " _
        + " and rc.id_encRecibo = cp.idEncRecibo " _
        + " and rc.estado = 1 " _
        + " and rv.via in (select via from rvias_pago pg where pg.simbolo = 2) " _
        + " union all " _
        + "select '          ' + rv.descripcion Partida, 'X'idRubro, sum(cp.importe*-1) importe " _
        + " from crecibo_pagos cp, rvias_pago rv, crecibo rc " _
        + " where(cp.idViaPago = rv.via)" _
        + " and rc.id_encRecibo = cp.idEncRecibo " _
        + " and rv.via in (select via from rvias_pago pg where pg.simbolo = 2) " _
        + " group by rv.descripcion " _
        + " union all " _
        + " select 	'TOTAL_DEPOSITO' Partida,'' idRubro, sum(p.importe) importe         " _
        + " from 	crecibo R left join crecibo_pagos P on id_EncRecibo = idEncRecibo   " _
        + " WHERE 	IDVIAPAGO<>'CR'  and R.Estado = 1 " _
        + " union all " _
        + " select 	'DEVOLUCION PT' Partida,'' idRubro, sum(importe*-1)importe    " _
        + " from [CBODEGA_LIQUIDACION]            " _
        + " where ttipo = 1   "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getCupones() As DataTable
        SQL_QUERY = " SELECT rp.idViaPago, sum(rp.importe) importe " _
        + " FROM crecibo r left join CRECIBO_PAGOS rp" _
        + " on r.id_encRecibo = rp.idEncRecibo WHERE rp.idViaPago in ('W','Y','Z') and r.estado = 1 " _
        + " GROUP BY rp.idViaPago "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDepositos() As DataTable
        SQL_QUERY = " select SHOWVALUE,DATAVALUE,idBanco,documento,importe,id_deposito " _
        + " from CDEPOSITO " _
        + " left join ttipo on  " _
        + " CONVERT(NVARCHAR(10), idBanco) = datavalue " _
        + " where tabla = 'BANCOS' "
        '+ " union  all "  _
        '+ " select '' showvalue, 0 as datavalue,  0 as idbanco, '' as documento, 0.0 as importe , 0 as id_deposito " _
        '+ " ORDER BY DATAVALUE DESC "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPreguntas(ByVal ttipo As String) As DataTable
        SQL_QUERY = " select * " _
        + " from [RSALIDA_INGRESO] where ttipo = " + ttipo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function setRespuesta(ByVal idPregunta As String, ByVal respuesta As String) As Integer
        SQL_QUERY = "  UPDATE RSALIDA_INGRESO SET " _
        + "  [fechaOperacion]		    	= GETDATE() " _
        + " ,[respuesta]	= '" + respuesta + "' " _
        + " WHERE id_SalidaIngreso = " + idPregunta
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setDeposito(ByVal idBanco As String, ByVal documento As String, ByVal valor As String) As Integer
        SQL_QUERY = "  INSERT INTO [CDEPOSITO]([idBanco],[documento],[importe])  " _
        + " VALUES (" + idBanco + ",'" + documento + "'," + valor + ") "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actualizarDeposito(ByVal idDeposito As String, ByVal idBanco As String, ByVal documento As String, ByVal valor As String) As Integer

        SQL_QUERY = " UPDATE CDEPOSITO SET  idBanco = " + idBanco _
        + " , documento = '" + documento + "'" _
        + " , importe = " + valor _
        + " WHERE id_deposito = " + idDeposito '+ "and idRuta = " + id_glo_ruta.ToString
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function eliminarDeposito(ByVal idDeposito As String) As Integer
        SQL_QUERY = " DELETE FROM CDEPOSITO " _
        + " WHERE id_deposito = " + idDeposito '+ "and idRuta = " + id_glo_ruta.ToString
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setActualizar(ByVal ruta As RutaCO) As Integer
        SQL_QUERY = "  UPDATE RRUTA SET " _
        + "  [esActual]	    	= '" + ruta.esActual.ToString + "'" _
        + " ,[confirmado]   	= '" + ruta.confirmado.ToString + "'" _
        + " ,[exportar]	    	= '" + ruta.exportar.ToString + "'" _
        + " ,[importar]	    	= '" + ruta.importar.ToString + "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setLiquidacion(ByVal integracion As IntegracionCO, ByVal rlayer As rLayerHandler) As Integer

        '--- Eliminar
        SQL_QUERY = " DELETE FROM CLIQUIDACION "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        With integracion
            '--- Agregar
            SQL_QUERY = " INSERT INTO [CLIQUIDACION]([ruta],[venta],[envaseRecibido],[creditoConcedido],[descuentoConcedido],[creditoCobrado],[devolucionProducto],[totalLiquidar],[diferenciaCorte],[moneda],[noBoleta],[valorBoleta],[importeDiferencia], [fechaEmision], zcorrelativo, idruta )  " _
            + " VALUES (" _
            + .RUTA _
            + "," + .VENTA _
            + "," + .ENVASE_RECIBIDO _
            + "," + .CRED_CONCEDIDOS _
            + "," + .DESC_CONCEDIDOS _
            + "," + .CRED_COBRADOS _
            + "," + .DEV_PRODUCTO _
            + "," + .TOTAL_LIQUIDAR _
            + "," + .DIF_CORTE _
            + ",'" + .MONEDA _
            + "','" + .NO_BOLETA_DEPOSITO _
            + "'," + .VALOR_BOLETA_DEPOSITO _
            + "," + .IMPORTE_DIFERENCIA _
            + ", GetDate()" _
            + ", CONVERT( nvarchar(9), GetDate(), 112 ) + REPLACE (CONVERT( nvarchar(5), GetDate(), 8  ),':','')  + CONVERT(NVARCHAR(5), " & .RUTA & ") " _
            + ",'" & id_glo_ruta & "'" _
            + " )"
        End With
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        Return SQL_RESULT
    End Function
End Class