Imports System.Data
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs

Public Class BodegaDT
    '--- ok
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable

    Dim objCe As New ceClient

    Public Function agregarItem(ByVal Inventario As inventarioCO) As Integer
        SQL_QUERY = "INSERT INTO CBODEGA_LIQUIDACION" _
                   + " (estado,idRuta,fechaOperacion,idBodega, " _
                   + "  idProducto,unidadesTeorico,unidadesFisico, " _
                   + " unidadesResumen,diferencia,importe,tTipo,unidadesRotura) " _
                   + "values('" + Inventario.estado + "','" + id_glo_ruta.ToString + "',getdate(),'" + Inventario.idBodega + "','" _
                   + Inventario.idProducto + "','" + Inventario.unidadesTeorico + "','" + Inventario.unidadesFisico + "','" _
                   + Inventario.unidadesResumen + "','" + Inventario.unidadesDiferencia + "','" + Inventario.importe + "','" + Inventario.tTipo + "','" + Inventario.unidadesRotura + "')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function obtenerEnvaseDevuelto(ByVal Inventario As inventarioCO) As DataTable
        SQL_QUERY = " " _
        + " SELECT " _
        + "	idproducto, descripcion, unidadesTeorico, unidadesResumen, unidadesFisico,diferencia, unidadesRotura Rotura,estado, fechaOperacion,abs(importe)importe, " _
        + "	convert(nvarchar(100),TCJ)+'/'+convert(nvarchar(100),TUN) TeoricoResumen,	 " _
        + "	convert(nvarchar(100),FCJ)+'/'+convert(nvarchar(100),FUN) FisicoResumen,  " _
        + "	convert(nvarchar(100), DCJ)+'/'+convert(nvarchar(100),DUN) DiferenciaResumen, " _
        + "	TCJ,TUN,FCJ,FUN,DCJ,DUN " _
        + " FROM " _
        + "         (  " _
        + "        	select " _
        + "			id_producto,	descripcion, " _
        + "			case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(ResumenTeorico))) end as TCJ, 			" _
        + "			case when unidadesCaja = 1 then           convert(nvarchar(100),ceiling(floor(ResumenTeorico))) else convert(nvarchar(100), ceiling ((ResumenTeorico - floor(ResumenTeorico))*unidadesCaja)) end as  TUN, 						" _
        + "			case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(ResumenFisico))) end as FCJ, 			" _
        + "			case when unidadesCaja = 1 then           convert(nvarchar(100),ceiling(floor(ResumenFisico))) else convert(nvarchar(100), ceiling ((ResumenFisico - floor(ResumenFisico))*unidadesCaja)) end as  FUN, 						" _
        + "			case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(ResumenDiferencia))) end as DCJ, 			" _
        + "			case when unidadesCaja = 1 then           convert(nvarchar(100),ceiling(floor(ResumenDiferencia))) else convert(nvarchar(100), ceiling ((ResumenDiferencia - floor(ResumenDiferencia))*unidadesCaja)) end as  DUN, 					" _
        + "			estado,	fechaOperacion, idproducto,  unidadesTeorico, unidadesResumen, unidadesFisico,diferencia,importe,unidadesRotura " _
        + "		from " _
        + "         (   " _
        + "        		select  " _
        + "				id_producto, descripcion, unidadesCaja, unidadesFisico,  " _
        + "				unidadesTeorico/convert(decimal,unidadesCaja) ResumenTeorico,  " _
        + "				unidadesFisico/convert(decimal,unidadesCaja) ResumenFisico,  " _
        + "				abs(diferencia)/convert(decimal,unidadesCaja) ResumenDiferencia,  " _
        + "				estado,	fechaOperacion,idproducto,  unidadesTeorico, unidadesResumen, diferencia,importe,unidadesRotura  " _
        + "			from  " _
        + "			[CBODEGA_LIQUIDACION] BL  " _
        + "			left join [RPRODUCTO]  " _
        + "			on id_producto = idproducto  " _
        + "     WHERE BL.idRuta = " + id_glo_ruta.ToString() _
        + "     AND BL.tTipo = " + Inventario.tTipo _
        + "     )QA  " _
        + ")QB  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function eliminarEnvaseDevuelto(ByVal Inventario As inventarioCO)
        SQL_QUERY = _
                "    DELETE " _
                + "  FROM CBODEGA_LIQUIDACION " _
                + "  WHERE idRuta = " + id_glo_ruta.ToString() _
                + "  AND tTipo = " + Inventario.tTipo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    '--------------------------------------------------------------------------------------------
    '--- OBTENER INFORMACION DEL CAMION                                                       ---
    '--------------------------------------------------------------------------------------------

    Public Function getTeoricoProducto() As DataTable
        SQL_QUERY = _
                  " SELECT 		id_producto, descripcion,cantidadActual,     " _
                & "                  	CASE WHEN unidadesCaja = 1 THEN '0/'+ convert(nvarchar(100),ceiling(floor(resumen))) ELSE convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja)) END AS cj_un, " _
                & "                  	cantidadActual cantidadReal, 0 Dif, 0 Rotura, ttipo ,categoria  " _
                & " FROM   " _
                & " 	( " _
                & " 		SELECT id_producto,descripcion,            " _
                & " 		case when cantidadActual is null then 0 else cantidadActual end as cantidadActual, " _
                & " 		case when cantidadActual is null then 0 else cantidadActual/convert(decimal,unidadesCaja) end as Resumen, " _
                & " 		unidadesCaja, ttipo, categoria   " _
                & " 		FROM   " _
                & " 		(  " _
                & " 			SELECT id_producto, descripcion, cantidad AS cantidadInicial,unidadesCaja, ttipo, categoria  " _
                & " 			FROM [MOVIMIENTO_INVENTARIO]  " _
                & " 				LEFT JOIN RPRODUCTO   " _
                & " 			ON idProducto = id_producto  " _
                & " 			WHERE origen = 'CARGA INICIAL'  " _
                & "             AND TTIPO IN (1,3) " _
                & " 			)QA " _
                & " 			LEFT JOIN    " _
                & " 			(   " _
                & " 			SELECT  idProducto, sum(cantidad) as cantidadActual " _
                & " 			FROM MOVIMIENTO_INVENTARIO   " _
                & " 			GROUP BY idProducto   " _
                & " 			)QD  " _
                & " 		ON QA.id_Producto = QD.idProducto " _
                & " 		)QA " _
                & " WHERE  cantidadActual > 0  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getTeoricoEnvase() As DataTable
        SQL_QUERY = _
                "   SELECT id_producto, descripcion,cantidadActual, " _
                + " CASE WHEN unidadesCaja = 1 THEN '0/'+ convert(nvarchar(100),ceiling(floor(resumen))) ELSE convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja)) END AS cj_un, " _
                + " cantidadActual cantidadReal, 0 Dif   ,precio  " _
                + " FROM(  		" _
                + " SELECT	" _
                + "    		id_producto, descripcion, sum(cantidad) cantidadActual, sum(cantidad)/convert(decimal,unidadesCaja) Resumen, P.unidadesCaja,avg(precio)precio " _
                + "	FROM	cNotaCredito NC LEFT JOIN cNotaCredito_detalle NCD  ON id_encNC = idencnc " _
                + "			LEFT JOIN rProducto	P  " _
                + "	ON		P.id_producto = NCD.idProducto 	" _
                + " WHERE NC.TTIPO = 4 and nc.estado = 1 " _
                + "	GROUP BY id_producto, descripcion, unidadesCaja " _
                + ")QA " _
                + "WHERE  cantidadActual > 0  ORDER BY cantidadActual DESC"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function crearFacturaDeCargo(ByVal tDocumento As String, ByVal rubro As String, ByRef idRecibo As String, ByRef idFactura As String, ByVal tipoLiquidacion As String)

        '--- Crear una conexion para esta transaccion
        Dim Conexion As SqlCeConnection
        Dim objUtil As New UtilitarioBL
        Conexion = objCe.dbConnect()
        Conexion.Open()

        '--- Query 1: insertar encabezado de la factura
        SQL_QUERY = " INSERT INTO [CFACTURA]([idRuta],[fSerie],[fNumero],[fechaEmision],[idCliente],[nit],[importe],[moneda],[importeDesto],[importeDestoPP],[importeDestoEnv],[nImpresiones],[porcentajeIva],[tTipo],[idusuario],[estado],[idEncRecibo])  " _
        + " SELECT idRuta , serie, actual, fechaEmision, idCliente, nit, sum(importe)importe, moneda, importeDesto, importeDestoPP, importeDestoEnv,  nImpresiones, porcentajeIva,  tTipo, idUsuario,  estado, idEncRecibo " _
        + " FROM( " _
        + " SELECT DISTINCT	" + id_glo_ruta.ToString() + " idRuta, serie,  actual, GETDATE() fechaEmision, " + id_glo_clienteGenerico.ToString() + " idCliente, 'C/F' nit, abs(importe)importe, '" + co_glo_moneda + "' moneda, 0 importeDesto, " _
        + "					0 importeDestoPP, 0 importeDestoEnv, 0 nImpresiones, " + co_glo_porcentajeIVA + " porcentajeIva, '" + tDocumento + "'  tTipo, " + id_glo_usuario.ToString() + " idUsuario, 1 estado,	 0 idEncRecibo " _
        + " 	FROM  [CBODEGA_LIQUIDACION] BL " _
        + " 	LEFT JOIN rccorrelativo CC " _
        + "     ON CC.idRuta = BL.idruta " _
        + "	    WHERE 	dtipo = 'F'  " _
        + "    	AND	diferencia < 0 " _
        + "     AND BL.tTipo = " + tipoLiquidacion _
        + " )QA " _
        + " GROUP BY idRuta ,	serie, actual, fechaEmision,  idCliente, nit, " _
        + " moneda, importeDesto, importeDestoPP, importeDestoEnv, nImpresiones, porcentajeIva,  tTipo, idUsuario,  estado, idEncRecibo "
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 2: Obtener el id del encabezado 
        SQL_QUERY = "SELECT @@IDENTITY as nn"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        idFactura = SQL_DT.Rows(0).Item(0).ToString

        '--- Query 3:  Crear el detalle de la factura
        SQL_QUERY = "  INSERT INTO [CFACTURA_DETALLE]([idEncFactura],[item],[idProducto],[cantidad],[um],[precio],[importe],[importeSinIva],[iva],[porcentajeDesto],[importeDesto],[tipoVenta],[porcentajeDestoPP],[importeDestoPP],[idRubro],[trqt],[litm],[estado])    " _
        + " SELECT idEncFactura, item, idProducto, cantidad,  um, precio,importe, importeSinIva, iva, porcentajeDesto, importeDesto, tipoVenta, porcentajeDestoPP, importeDestoPP, idRubro,convert(nvarchar(100),ceiling(floor(ResumenDiferencia)))+'/'+convert(nvarchar(100), ceiling ((ResumenDiferencia - floor(ResumenDiferencia))*unidadesCaja)) trqt	,idproducto litm	, estado   " _
        + " FROM( " _
        + "     SELECT 	 " + idFactura + " idEncFactura 	 ,1 item	,idProducto	,abs(diferencia) cantidad	,'UM' um ,abs(importe/diferencia/(" + (1 + objUtil.isDecimal(1 + co_glo_porcentajeIVA)).ToString + ")) precio	,abs(importe) importe, abs(importe)/" + co_glo_porcentajeIVA.ToString + " importeSinIva, " + co_glo_porcentajeIVA + " iva	,0 porcentajeDesto	,0 importeDesto " _
        + "   	,'" + tDocumento + "' tipoVenta	,0 porcentajeDestoPP	,0 importeDestoPP	,'" + rubro + "' idRubro	,unidadesResumen trqt	,idproducto litm	,1 estado , abs(diferencia)/convert(decimal,unidadesCaja) ResumenDiferencia, unidadesCaja  " _
        + "     FROM 	[CBODEGA_LIQUIDACION]  BL  left join RPRODUCTO on id_producto = idproducto" _
        + "     WHERE 	diferencia < 0  AND BL.tTipo =  " + tipoLiquidacion _
        + " )QA "
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)


        '---------------------- RECIBO ------------------------'
        '--- Query 1: insertar encabezado del recibo ----'
        SQL_QUERY = " INSERT INTO [CRECIBO]([idRuta],[rSerie],[rNumero],[fechaEmision],[idCliente],[importe],[porcentajeDesto],[importeDesto],[importeDestoEnv],[nImpresiones],[idusuario],[doTipo],[estado],[idEncFactura],[idEncCxc])  " _
        + " SELECT idRuta , serie, numero, fechaEmision, idCliente, sum(importe)importe, 0 porcentajeDesto, 0 importeDesto, 0 importeDestoEnv, 0 nImpresiones, idUsuario,  doTipo,  1 estado, " + idFactura + " idEncFactura,0 idEncCxc " _
        + " FROM(   SELECT DISTINCT	" + id_glo_ruta.ToString() + " idRuta, serie, actual as numero, GETDATE() fechaEmision,  " + id_glo_clienteGenerico.ToString() + "  idCliente, abs(importe)importe,  '" + tDocumento + "' doTipo, " + id_glo_usuario.ToString() + " idUsuario " _
        + " 		FROM  			[CBODEGA_LIQUIDACION] BL  	LEFT JOIN rccorrelativo CC      ON CC.idRuta = BL.idruta 	    " _
        + " 		WHERE 	dtipo = 'R' 		AND	diferencia < 0 " _
        + "         AND BL.tTipo = " + tipoLiquidacion _
        + " )QA  GROUP BY idRuta ,	serie, numero, fechaEmision,  idCliente,     doTipo, idUsuario "
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 2: Obtener el id del encabezado      ----'
        SQL_QUERY = "SELECT @@IDENTITY as nn"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        idRecibo = SQL_DT.Rows(0).Item(0).ToString


        SQL_QUERY = " INSERT INTO [CRECIBO_DETALLE]([idEncRecibo],[linea],[dTipo],[serie],[numero],[importe],[saldo],[estado])  " _
        + " SELECT DISTINCT	" + idRecibo + "idEncRecibo, 1 linea,  '" + tDocumento + "' dTipo, serie, actual as numero, abs(sum(importe))importe,0 saldo, 1 estado " _
        + " FROM  			[CBODEGA_LIQUIDACION] BL  	LEFT JOIN rccorrelativo CC      ON CC.idRuta = BL.idruta 	     		 " _
        + " WHERE 	dtipo = 'F' 		AND	diferencia < 0          AND BL.tTipo = " + tipoLiquidacion _
        + " group by serie,actual"

        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 4: agregar el pago        ----'
        SQL_QUERY = " INSERT INTO [CRECIBO_PAGOS]([idEncRecibo],[idViaPago],[importe],[documento],[idInstitucion],[moneda],[estado])  " _
        + " SELECT DISTINCT	" + idRecibo + "  idEncRecibo, 'E'  idViaPago,  abs(SUM(importe)) importe,0 documento, 0 idInstitucion,  '" + co_glo_moneda + "' moneda, 1 estado " _
        + " FROM  			[CBODEGA_LIQUIDACION] BL  " _
        + " WHERE   		diferencia < 0  AND BL.tTipo = " + tipoLiquidacion
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        '-------------------- </RECIBO> -----------------------'

        '--- Incrementar el correlativos
        SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'F'"
        objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'R'"
        objCe.SetExecute(SQL_QUERY)

        '--- Cerrar la conexion
        Conexion.Close()
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function getDiferenciaConteo(ByVal ttipo) As DataTable
        SQL_QUERY = _
               "    SELECT  idproducto, descripcion, unidadesTeorico, unidadesResumen, unidadesFisico,diferencia" _
               + "  FROM    CBODEGA_LIQUIDACION BL " _
               + "  WHERE BL.tTipo = " + ttipo _
               + "  AND diferencia <0"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getEnvasePT() As DataTable
        SQL_QUERY = _
          " SELECT  1 AS estado, GETDATE() fechaOperacion, 1 idbodega, P.id_producto, L.unidadesTeorico, L.unidadesFisico, L.unidadesResumen, L.Diferencia, 0 as importe, L.ttipo, L.unidadesRotura " _
        & " FROM [CBODEGA_LIQUIDACION] L " _
        & " left join boom B " _
        & " ON B.idProducto = L.idProducto " _
        & " left join rproducto P ON B.idExplosion = id_producto " _
        & " WHERE  P.ttipo =2 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

End Class



