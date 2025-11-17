Imports System.Data
Imports Proyecto_xoMobile_Packs
Public Class ClienteDT
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim objCe As New ceClient

    ''---ok
    Public Function getListado(ByVal dia As String)

        If tipoRuta <> "16" Then
            '& "  SELECT  '" & id_glo_clienteGenerico & "' as codigo,  'CLIENTE VARIOS' as descripcion, 'RUTA " & id_glo_codRuta & "' direccion, '0' telefono, '0' visitado, '0' orden ,0 frecuencia, 0 semana , 0 saldo, 'true' visitar  " _
            '& "  UNION " _
            SQL_QUERY = "" _
            & "  SELECT 	*,   " _
            & "	    CASE 	WHEN frecuencia = 1 and semana =  1 then 'true'   " _
            & "				WHEN frecuencia = 1 and semana =  3 then 'true'   " _
            & "				WHEN frecuencia = 2 and semana =  2 then 'true'   " _
            & "				WHEN frecuencia = 2 and semana >= 4 then 'true'   " _
            & "				WHEN frecuencia = 0 then 'true'                  " _
            & "				ELSE   'false'	END as visitar			         " _
            & "  FROM (" _
            & " 	SELECT  convert(nvarchar(25),id_cliente) as codigo,  negocio as descripcion, direccion, telefono, visitado, orden,frecuencia, DATEPART(DAY, (GETDATE()) - 1  ) / 7 + 1 AS semana, CASE WHEN SUM(saldo) is null then 0 else SUM (saldo) END as saldo " _
            & " 	FROM rcliente  " _
            & "		LEFT JOIN rcxc ON id_cliente = idcliente    " _
            & "     WHERE diaVisita = " & dia _
            & "		GROUP BY id_cliente, negocio , direccion, telefono, visitado, orden , frecuencia " _
            & " )QA order by orden "
        Else
            SQL_QUERY = "" _
            & "  SELECT 	*,   " _
            & "	    CASE 	WHEN frecuencia = 1 and semana =  1 then 'true'   " _
            & "				WHEN frecuencia = 1 and semana =  3 then 'true'   " _
            & "				WHEN frecuencia = 2 and semana =  2 then 'true'   " _
            & "				WHEN frecuencia = 2 and semana >= 4 then 'true'   " _
            & "				WHEN frecuencia = 0 then 'true'                  " _
            & "				ELSE   'false'	END as visitar			         " _
            & "  FROM (" _
            & " 	SELECT  convert(nvarchar(25),id_cliente) as codigo,  negocio as descripcion, direccion, telefono, visitado, orden,frecuencia, DATEPART(DAY, (GETDATE()) - 1  ) / 7 + 1 AS semana, CASE WHEN SUM(saldo) is null then 0 else SUM (saldo) END as saldo " _
            & " 	FROM rcliente  " _
            & "		LEFT JOIN rcxc ON id_cliente = idcliente    " _
            & "		GROUP BY id_cliente, negocio , direccion, telefono, visitado, orden , frecuencia " _
            & " )QA order by orden "
        End If
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getReferenciaFEL(ByVal idRecibo As String)
        SQL_QUERY = _
          "  select cx.tipoidrecep, cx.idrecep, cx.seriefel, cx.numeroautorizacion from cnotacredito nc, crecibo rec, rcxc cx " _
        & " where nc.idEncRecibo = rec.id_EncRecibo and rec.idEncCXC = cx.id_cxc and rec.id_encRecibo = '" & idRecibo & "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getCredenciales()
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        '"  SELECT  servidor, servicio, protocolo FROM RCREDENCIALES WHERE INTERNET = 0"
        SQL_QUERY = _
        "  SELECT  servidor, servicio, protocolo FROM RCREDENCIALES"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    

    Public Function getCredencialesI()
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT  servidor, servicio, protocolo FROM RCREDENCIALES WHERE INTERNET = 1"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getListaPrecioCliente(ByVal idCliente As String)
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT  listaPrecio FROM rcliente where id_cliente = '" & idCliente & "' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getProductoListaPrecio(ByVal id_producto As String, ByVal listaPrecios As String)
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT  * FROM zcampos2 where id_producto like '%" & id_producto & "%' and listaPrecios = " & listaPrecios
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function



    Public Function getDatosElectronicos(ByVal idFactura As Integer)
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT  NUMEROAUTORIZACION, PREIMPRESO FROM CFACTURA WHERE ID_ENCFACTURA = " & idFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDatosElectronicosCXC(ByVal idFactura As Integer)
        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT cxc.numeroautorizacion, cxc.seriefel PREIMPRESO FROM cnotacredito nc, crecibo rc, rcxc cxc WHERE nc.idEncRecibo = rc.id_encRecibo AND rc.idEncCXC = cxc.id_cxc AND nc.id_EncNC = " & idFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDetalleZsearch(ByVal idCliente As String)
        SQL_QUERY = _
       "SELECT   CONVERT(NVARCHAR(25),id_cliente)id_cliente, categoria categoriaC " _
       + "FROM rcliente " _
       + "WHERE id_cliente = " + idCliente
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDetalleCliente(ByVal idCliente As String, ByRef rlayer As rLayerHandler)
        'SQL_QUERY = _
        ' "   select rcliente.* , n_claseCliente, n_region_s,n_canal,n_tipoRuta  " _
        '& " from rcliente " _
        '& " left join  " _
        '         & " ( " _
        '        & " 	select showValue as n_claseCliente, datavalue " _
        '       & " 	from ttipo " _
        '      & " 	where tabla = 'SEGMENT_CLASE' " _
        '     & " )A on A.datavalue =  claseCliente " _
        '    & " 	left join  " _
        '   & " 	( " _
        '  & " 		select showValue as n_region_s, datavalue " _
        ' & " 		from ttipo " _
        '& " 		where tabla = 'SEGMENT_REGION' " _
        '         & " 	)B on B.datavalue =  region_s " _
        '        & " 		left join  " _
        '       & " 		( " _
        '      & " 			select showValue as n_canal , datavalue " _
        '     & " 			from ttipo " _
        '    & " 			where tabla = 'SEGMENT_CANAL' " _
        '   & " 		)C on C.datavalue =  canal " _
        '  & " 			left join  " _
        ' & " 			( " _
        '& " 				select showValue as n_tipoRuta , datavalue " _
        '         & " 				from ttipo " _
        '        & " 				where tabla = 'SEGMENT_RUTA' " _
        '       & " 			)D on D.datavalue =  tipoRuta " _
        '      & " WHERE id_cliente = " + idCliente
        '    SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)

        SQL_QUERY = _
        "   select rcliente.* , n_claseCliente, n_region_s,n_canal,n_tipoRuta  " _
        & " from rcliente " _
        & " left join  " _
        & " ( " _
        & " 	select showValue as n_claseCliente, tabla " _
        & " 	from ttipo " _
        & " )A on A.tabla =  claseCliente " _
        & " 	left join  " _
        & " 	( " _
        & " 		select showValue as n_region_s, tabla " _
        & " 		from ttipo " _
        & " 	)B on B.tabla =  region_s " _
        & " 		left join  " _
        & " 		( " _
        & " 			select showValue as n_canal , tabla " _
        & " 			from ttipo " _
        & " 		)C on C.tabla =  canal " _
        & " 			left join  " _
        & " 			( " _
        & " 				select showValue as n_tipoRuta , tabla " _
        & " 				from ttipo " _
        & " 			)D on D.tabla =  tipoRuta " _
        & " WHERE id_cliente = " + idCliente
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getViasPagoContado() As DataTable
        SQL_QUERY = _
        " SELECT	VP.via id_viaPago, vp.descripcion as via_pago, '' as valor, '' as documento, '' as banco, 0 as dataBanco " _
        + " FROM	rcliente C " _
        + "	 LEFT JOIN	rasigna_vias_pago AVP  " _
        + "	 on C.id_cliente = AVP.idCliente " _
        + "	 	LEFT JOIN rvias_pago VP " _
        + "	 	on VP.via = AVP.idViaPago " _
        + " WHERE	C.id_cliente  = " + id_glo_cliente.ToString() _
        + " AND		C.idRuta		= " + id_glo_ruta.ToString() _
        + " AND VP.via <> 'CR' ORDER BY AVP.idViaPago ASC "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    '---xo2.0
    Public Function getAllViasPago() As DataTable
        SQL_QUERY = _
          " SELECT 'E' id_viaPago,'Efectivo' via_pago,'' valor ,'' documento,'' banco, 0 dataBanco  " _
        & " union all " _
        & " SELECT	VP.via id_viaPago, vp.descripcion as via_pago, '' as valor, '' as documento, '' as banco, 0 as dataBanco " _
        & " FROM	rcliente C " _
        & "	 LEFT JOIN	rasigna_vias_pago AVP  " _
        & "	 on C.id_cliente = AVP.idCliente " _
        & "	 	LEFT JOIN rvias_pago VP " _
        & "	 	on VP.via = AVP.idViaPago " _
        & " WHERE	idviaPago <> 'E' and C.id_cliente  = " + id_glo_cliente.ToString() _
        & " order by via_pago desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getViaCredito() As DataTable
        SQL_QUERY = _
        " SELECT	VP.via id_viaPago, vp.descripcion as via_pago, '' as valor, '' as documento, '' as banco, 0 as dataBanco " _
        + " FROM	rcliente C " _
        + "	 LEFT JOIN	rasigna_vias_pago AVP  " _
        + "	 on C.id_cliente = AVP.idCliente " _
        + "	 	LEFT JOIN rvias_pago VP " _
        + "	 	on VP.via = AVP.idViaPago " _
        + " WHERE	C.id_cliente  = " + id_glo_cliente.ToString() _
        + " AND		C.idRuta		= " + id_glo_ruta.ToString() _
        & " AND  VP.via  = 'CR' " _
        + " order by via_pago desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getViasPagoGenerico() As DataTable
        SQL_QUERY = " SELECT 'E' id_viaPago,'EFECTIVO' via_pago,'' valor ,'' documento,'' banco, 0 dataBanco " 'Permite activar la forma de pago en efectivo siempre
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getClientesConSaldo() As DataTable
        SQL_QUERY = _
       " SELECT idCliente as codigo,negocio, propietario, direccion,telefono, sum(importe) TotalImporte, sum(saldo)TotalSaldo, avg(datediff (dd,fechaVence,fechaEmision))DiasVencidosPromedio, " _
       + " case when diaVisita = 1 then 'lunes' " _
       + "      when diaVisita = 2 then 'martes' " _
       + "	    when diaVisita = 3 then 'miercoles'" _
       + "	    when diaVisita = 4 then 'jueves'" _
       + "	    when diaVisita = 5 then 'viernes'" _
       + "      when diaVisita = 6 then 'sabado'" _
       + "	    when diaVisita = 7 then 'domingo'" _
       + " end as diaVisita, diaVisita as ndiavisita , orden " _
       + " FROM rcxc" _
       + " left join Rcliente" _
       + " on idcliente=id_cliente" _
       + " WHERE saldo <> 0" _
       + " group by idCliente,negocio, direccion,telefono, propietario,diaVisita,orden " _
       + " order by ndiavisita, orden asc"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPresupuesto(ByVal idCliente As String) As DataTable
        SQL_QUERY = _
       "    SELECT * " _
       + "  FROM " _
       + "  rpresupuesto " _
       + "	WHERE   idCliente = " + idCliente
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function editar(ByVal cliente As ClienteCO) As Integer
        SQL_QUERY = "" _
        + "UPDATE [RCLIENTE] " _
        + "SET  " _
        + " [visitado] = '" + cliente.visitado + "'" _
        + ",[razonNoVisita] =  '" + cliente.razonNoVisita + "'" _
        + ",[creditoDisponible] =  '" + cliente.creditoDisponible + "'" _
        + ",[razonNoVenta] =  '" + cliente.razonNoVenta + "'" _
        + " WHERE" _
        + "[id_cliente] = " + cliente.codigo
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function


    Public Function tipoReceptor(ByVal tReceptor As String, ByVal idReceptor As String, ByVal idFactura As String) As Integer
        SQL_QUERY = "" _
        + "UPDATE [CFACTURA] SET " _
        + " tipo_receptor = '" & tReceptor & "', " _
        + " idreceptor = '" & idReceptor & "'" _
        + " WHERE " _
        + " id_encFactura = '" & idFactura & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function tipoReceptorNC(ByVal tReceptor As String, ByVal idReceptor As String, ByVal idNC As String) As Integer
        SQL_QUERY = "" _
        + "UPDATE [CNOTACREDITO] SET " _
        + " tipo_receptor = '" & tReceptor & "', " _
        + " idreceptor = '" & idReceptor & "'" _
        + " WHERE " _
        + " id_encNc = '" & idNC & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function editarDatosFEL(ByVal cliente As ClienteCO) As Integer
        SQL_QUERY = "" _
        + "UPDATE [RCLIENTE] " _
        + "SET  " _
        + " [nit] = '" + cliente.nit + "'" _
        + " ,[actualiza_cui] = 'X'" _
        + ",[numeroDi] =  '" + cliente.numeroDi + "'" _
        + " WHERE" _
        + "[id_cliente] = " + cliente.codigo
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setCreditoDisponible() As Boolean
        '--- 1 Tabla tmp para almacenar clientes con saldo 
        SQL_QUERY = "" _
        & " CREATE TABLE [TEMP_RCLIENTE] ([sociedad] nvarchar(5) NULL   " _
        & " , [id_cliente] int  NOT NULL   " _
        & " , [ramo] nvarchar(5) NULL   " _
        & " , [ruta] int  NULL   " _
        & " , [grupoVentas] nvarchar(4) NULL   " _
        & " , [region] nvarchar(4) NULL   " _
        & " , [negocio] nvarchar(200) NULL   " _
        & " , [propietario] nvarchar(200) NULL   " _
        & " , [nit] nvarchar(15) NULL   " _
        & " , [tipoDi] nvarchar(5) NULL   " _
        & " , [numeroDi] nvarchar(25) NULL   " _
        & " , [direccion] nvarchar(100) NULL   " _
        & " , [telefono] nvarchar(20) NULL   " _
        & " , [patente] nvarchar(10) NULL   " _
        & " , [categoria] nvarchar(5) NULL   " _
        & " , [condicion] nvarchar(5) NULL   " _
        & " , [creditoAutorizado] numeric(13,2) NULL   " _
        & " , [creditoDisponible] numeric(13,2) NULL   " _
        & " , [diasCredito] int  NULL   " _
        & " , [diasCreditoPresupuesto] int  NULL   " _
        & " , [volPresupuesto] numeric(13,3) NULL   " _
        & " , [ventaConSaldo] bit  NULL   " _
        & " , [ventaConSaldov] bit  NULL   " _
        & " , [exVentaContado] bit  NULL   " _
        & " , [diaVisita] int  NULL   " _
        & " , [orden] int  NULL   " _
        & " , [nFaltas] int  NULL   " _
        & " , [visitado] int  NULL   " _
        & " , [razonNoVisita] nvarchar(5) NULL   " _
        & " , [razonNoVenta] int  NULL   " _
        & " , [idRuta] int  NULL   " _
        & " , [listaPrecio] nvarchar(6) NULL  " _
        & " , [oficinaVentas] nvarchar(10) NULL  " _
        & " , [departamento] nvarchar(10) NULL   " _
        & " , [claseCliente] nvarchar(2) NULL   " _
        & " , [region_s] nvarchar(10)  NULL   " _
        & " , [canal] nvarchar(10)  NULL   " _
        & " , [tipoRuta] nvarchar(10)  NULL   " _
        & " , [frecuencia] int        NULL   " _
        & " , [longitud] nvarchar(15) NULL   " _
        & " , [latitud] nvarchar(15) NULL   " _
        & " , [deptoMuni] nvarchar(50) NULL   " _
        & " , [vale_bon] nvarchar(1)        NULL   " _
        & " , [reclamo] nvarchar(1)        NULL   " _
        & " , [devolucion] nvarchar(1)        NULL   " _
        & " , [ramo5] nvarchar(50)        NULL   " _
        & " , [actualiza_cui] nvarchar(1)        NULL   " _
        & " ); "
        ' & " , [georeferencia] nvarchar(10)  NULL   " _
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- 2 Insertar clientes deudores calculando su credito disponible
        SQL_QUERY = "" _
        & " INSERT INTO 	[TEMP_RCLIENTE]([sociedad],[id_cliente],[ramo],[ruta],[grupoVentas],[region],[negocio],[propietario],[nit],[tipoDi],[numeroDi],[direccion],[telefono],[patente],[categoria],[condicion],[creditoAutorizado],                            [creditoDisponible],[diasCredito],[diasCreditoPresupuesto],[volPresupuesto],[ventaConSaldo],[ventaConSaldov],[exVentaContado],[diaVisita],[orden],[nFaltas],[visitado],[razonNoVisita],[razonNoVenta],[idRuta],[listaPrecio],[oficinaVentas],[departamento],[claseCliente],[region_s],[canal],[tipoRuta],[frecuencia],[latitud],[longitud],[deptoMuni], [vale_bon], [reclamo], [devolucion],[ramo5],[actualiza_cui])  " _
        & " SELECT   		                [sociedad],[id_cliente],[ramo],[ruta],[grupoVentas],[region],[negocio],[propietario],[nit],[tipoDi],[numeroDi],[direccion],[telefono],[patente],[categoria],[condicion],[creditoAutorizado],(creditoAutorizado - saldo) [creditoDisponible],[diasCredito],[diasCreditoPresupuesto],[volPresupuesto],[ventaConSaldo],[ventaConSaldov],[exVentaContado],[diaVisita],[orden],[nFaltas],[visitado],[razonNoVisita],[razonNoVenta],[idRuta],[listaPrecio],[oficinaVentas],[departamento],[claseCliente],[region_s],[canal],[tipoRuta],[frecuencia],[latitud],[longitud],[deptoMuni], [vale_bon], [reclamo], [devolucion],[ramo5],[actualiza_cui] " _
        & " FROM 			rcliente " _
        & " left join   " _
        & " 	( " _
        & " 	 SELECT	DISTINCT idCliente,sum(saldo) saldo " _
        & " 	 FROM 	RCXC " _
        & " 	 GROUP 	BY idCliente " _
        & " 	)QA " _
        & " ON 		idcliente = id_cliente " _
        & " WHERE 	saldo <>0 and creditoAutorizado >0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- 3 Eliminar registros de la tabla original
        SQL_QUERY = " DELETE FROM RCLIENTE  WHERE id_cliente IN (SELECT id_cliente FROM TEMP_RCLIENTE) "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- 3.5 Actualizar el limite de credito para clientes que no tienen saldo
        SQL_QUERY = " UPDATE rcliente SET creditoDisponible = creditoAutorizado  WHERE creditoAutorizado <> 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- 4 Agregar registros calculados a tabla original
        SQL_QUERY = " INSERT INTO RCLIENTE SELECT * FROM TEMP_RCLIENTE "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- 5 Eliminar tabla temporal
        SQL_QUERY = " DROP TABLE TEMP_RCLIENTE "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return True
    End Function

    Public Function getRangoDiasCredito(ByVal categoria As String)

        '--- Devuelve la matriz de presupuestos para la categoria de cliente
        SQL_QUERY = _
        "  SELECT  getdate(),* FROM rrango WHERE categoriac LIKE '%" + categoria + "%' AND  CONVERT(NVARCHAR(10),GETDATE(),121)  >=  CONVERT(NVARCHAR(10),fechaInicio,121)  and  CONVERT(NVARCHAR(10),GETDATE(),121)   <= CONVERT(NVARCHAR(10),fechafin,121)    "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function crear(ByVal dtCliente As DataTable) As Integer
        SQL_QUERY = "" _
        & "INSERT INTO [RCLIENTE]([sociedad],[id_cliente],[negocio],[propietario],[nit],[tipoDi],[numeroDi],[direccion],[idRuta],[latitud],[longitud],[deptoMuni],[DIAVISITA],[frecuencia],[visitado],[listaPrecio],[condicion],[categoria],[ramo])" _
        & "VALUES ('" & id_glo_sociedad & "','" & dtCliente.Rows(0).Item("CLIE_HH").ToString & "','" & dtCliente.Rows(0).Item("NOMBRE1").ToString & dtCliente.Rows(0).Item("NOMBRE2").ToString & "','" & dtCliente.Rows(0).Item("NOMBRE3").ToString & "','" & dtCliente.Rows(0).Item("NIT").ToString & "','" & "NUEVO" & "','" & dtCliente.Rows(0).Item("DPI").ToString & " ','" & dtCliente.Rows(0).Item("DIRECCION").ToString & "','" & id_glo_ruta & "','" & dtCliente.Rows(0).Item("LATITUDE").ToString & "','" & dtCliente.Rows(0).Item("LONGITUDE").ToString & "','" & dtCliente.Rows(0).Item("POBLAC").ToString & "','" & id_glo_dia & "','0','0','10','IL01','08','" & dtCliente.Rows(0).Item("giro").ToString & "')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function cambiarResponsable(ByVal clienteT As String, ByVal clienteSAP As String) As Integer
        '--- CAMBIAR ESTATUS DE CLIENTE NUEVO
        SQL_QUERY = _
       " UPDATE 	RCLIENTE  SET 		TIPODI = 'TEMP'  WHERE id_cliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
        " UPDATE 	RCLIENTE  SET 		id_cliente = " & clienteSAP & "  WHERE id_cliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)


        Return SQL_RESULT
    End Function

    Public Function getNuevos(ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " SELECT * FROM RCLIENTE WHERE TIPODI='NUEVO' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function
End Class

