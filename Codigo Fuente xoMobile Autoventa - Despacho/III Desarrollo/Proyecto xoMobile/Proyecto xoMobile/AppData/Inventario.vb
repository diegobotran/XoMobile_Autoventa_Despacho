Imports System.Data
Imports Proyecto_xoMobile_Packs

'--- ok
Public Class Inventario

    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable

    Dim objCe As New ceClient

    Public Function agregarItem(ByVal comItem As ItemCO) As Integer
        With comItem
            SQL_QUERY = "" _
        + " INSERT INTO [CINVENTARIO_FISICO]([idRuta],[idCliente],[idProducto],[um],[cantidad],[importeSinIva],[importe],[iva],[fechaEmision],[estado],[idusuario],[idRubro],[trqt],[litm])  " _
        + " VALUES ('" + id_glo_ruta.ToString + "','" + id_glo_cliente.ToString() + "','" + .idProducto + "','" + .um + "','" + .cantidad + "','" + .importeSinIva + "','" + .importe + "','" + .iva + "',getdate(),'" + .estado + "','" + id_glo_usuario.ToString + "','" + .idRubro + "','" + .trqt + "','" + .litm + "')"
        End With
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        If SQL_RESULT <= 0 Then
            Throw New Exception("No se insertaron registros en la ultima operacion. Ejecute nuevamente la toma de invnetario.")
        End If
        Return SQL_RESULT
    End Function

    '--- xoMobile 2.0
    Public Function confirmar(ByVal sd As String, ByRef rlayer As rLayerHandler) As Integer

        '--- Eliminar registros de carga inicial
        SQL_QUERY = " " _
        + "   DELETE FROM MOVIMIENTO_INVENTARIO" _
        + "   WHERE ORIGEN  =  'CARGA INICIAL' "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- ingresar los movimientos de inventario como ingresos (+)
        SQL_QUERY = " " _
        + " INSERT INTO  MOVIMIENTO_INVENTARIO([idRuta],[idCliente],[idProducto],[um],[cantidad],[origen])   " _
        + " SELECT idRuta,0,idProducto,um,sum(cantidadInicial) cantidadInicial ,'CARGA INICIAL' " _
        + " FROM " _
        + " ( " _
        + " 	SELECT idRuta, idProducto,um,cantidadInicial , 1 control   FROM RCARGA_LOG where confirmada = 'false'" _
        + " )QA " _
        + " GROUP BY  idRuta, idProducto,um, control "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)

        '--- Actualizar estado de la carga a confirmado 
        SQL_QUERY = " " _
        + "   UPDATE RCARGA_LOG SET confirmada = 'true' " _
        + "   WHERE sd =" + sd
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)


        Return SQL_RESULT
    End Function

    '---xoMobile 2.0
    Public Function setMovimiento(ByVal comItem As ItemCO, ByVal origen As String, ByRef rlayer As rLayerHandler) As Integer
        With comItem
            SQL_QUERY = "" _
        & " INSERT INTO [MOVIMIENTO_INVENTARIO]([idRuta],[idCliente],[idProducto],[um],[cantidad],[origen])  " _
        & " VALUES ('" & id_glo_ruta.ToString & "','" & id_glo_cliente.ToString() & "','" & .idProducto & "','" & .um & "','" & .cantidad & "','" & origen & "')"
        End With
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        Return SQL_RESULT
    End Function

    '--- xoMobile 2.0

    '--- ESE UNION ALL NO ME PARECE PORQUE ESTA DUPLICANDO EL INICIAL
    'Public Function getInventarioActual(ByRef rlayer As rLayerHandler) As DataTable
    '    '& " SELECT   CONVERT(NVARCHAR(25), id_producto) as id_producto,descripcion,QA.cantidadInicial , QA.cantidadActual, QA.um,  QA.cantidadActual  as teorico, QA.cantidadActual as fisico, 0 as diferencia, P.ttipo, 0 as Devuelve " _
    '    SQL_QUERY = "" _
    '    & " SELECT   CONVERT(NVARCHAR(25), id_producto) as id_producto,descripcion,QA.cantidadInicial/p.unidadesCaja CantidadInicial , QA.cantidadActual/p.unidadesCaja cantidadActual, QA.um,  QA.cantidadActual/p.unidadesCaja  as teorico, QA.cantidadActual/p.unidadesCaja as fisico, 0 as diferencia, P.ttipo, 0 as Devuelve " _
    '    & " FROM 	RPRODUCTO P LEFT JOIN      " _
    '    & " (   " _
    '    & " 	SELECT Q.*,C.cantidadInicial   " _
    '    & " 	FROM RCARGA_LOG C    " _
    '    & " 	LEFT JOIN    " _
    '    & " 	(   " _
    '    & " 		SELECT A.idProducto, SUM(A.cantidad) cantidadActual ,um " _
    '    & " 		FROM     " _
    '    & " 		(     " _
    '    & " 			SELECT idProducto, cantidadInicial cantidad , um " _
    '    & " 			FROM RCARGA_LOG               " _
    '    & " 			UNION ALL      " _
    '    & " 			SELECT  idProducto,cantidad, um " _
    '    & " 			FROM [MOVIMIENTO_INVENTARIO]            	   " _
    '    & " 		)A     " _
    '    & " 		GROUP BY A.idProducto, A.um " _
    '    & " 	)Q   " _
    '    & " 	ON Q.idProducto = C.idProducto   " _
    '    & " )QA   " _
    '    & " ON QA.idProducto = P.id_producto   " _
    '    & " WHERE 	 cantidadInicial >0             " _
    '    & " ORDER BY id_producto ASC "
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
    '    Return SQL_DT
    'End Function

    '---xoMobile 2.0
    Public Function getInventarioEnvase(ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "  SELECT id_producto , descripcion, 0 as cantidadInicial,	sum(cantidad)cantidadActual, RC.um, sum(cantidad) teorico, sum(cantidad) as fisico, 0 as diferencia, NC.ttipo " _
        & "  FROM  " _
        & "  	[CNOTACREDITO] NC LEFT JOIN   " _
        & "  	[CNOTACREDITO_DETALLE]	 RC   " _
        & "  on id_EncNC = idEncNC   " _
        & "  LEFT JOIN rproducto	RP    " _
        & "  ON	RP.id_producto = RC.idProducto	   " _
        & "  WHERE nc.estado = 1  " _
        & "  GROUP BY id_producto, descripcion,RP.unidadesCaja, NC.ttipo, um "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getMovimientos(ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "  SELECT id_producto,descripcion,  " _
        & "  case when cantidadInicial is null then 0 else cantidadInicial end as cantidadInicial,  " _
        & "  case when cantidadVendida is null then 0 else cantidadVendida end as cantidadVendida,  " _
        & "  case when cantidadDevolucion is null then 0 else abs(cantidadDevolucion) end as cantidadDevolucion,  " _
        & "  case when cantidadActual is null then 0 else cantidadActual end as cantidadActual,  " _
        & "  unidadesCaja, " & id_glo_codRuta.ToString & " ruta, 0 as diferencia, zcorrelativo," _
        & "  case when (cantidadVendida * litrosUnidad) is null then 0 else (cantidadVendida * litrosUnidad) end as litrosVendido, ttipo " _
        & "  FROM  " _
        & "  ( " _
        & "  	SELECT id_producto, descripcion, cantidad AS cantidadInicial,unidadesCaja, idruta, litrosUnidad, tTipo " _
        & "  	FROM [MOVIMIENTO_INVENTARIO] " _
        & "  		LEFT JOIN RPRODUCTO  " _
        & "  		ON idProducto = id_producto " _
        & "  		WHERE origen = 'CARGA INICIAL' " _
        & "  )QA  " _
        & "  	LEFT JOIN   " _
        & "  	(  " _
        & "  		SELECT  idProducto, abs(sum(cantidad)) as cantidadVendida    " _
        & "  		FROM MOVIMIENTO_INVENTARIO WHERE  origen IN  ('ODV','ANULA-ODV','ZTAP') " _
        & "  		GROUP BY idProducto  " _
        & "  	)QB  " _
        & "  	ON QA.id_Producto = QB.idProducto " _
        & "  		LEFT JOIN   " _
        & "  		(  " _
        & "  				SELECT  idProducto, sum(cantidad) as cantidadDevolucion  " _
        & "  				FROM MOVIMIENTO_INVENTARIO WHERE  origen IN  ('DEVOLUCION')  " _
        & "  				GROUP BY idProducto  " _
        & "  		)QC " _
        & "  		ON QA.id_Producto = QC.idProducto " _
        & "  			LEFT JOIN   " _
        & "  			(  " _
        & "  					SELECT  idProducto, sum(cantidad) as cantidadActual  " _
        & "  					FROM MOVIMIENTO_INVENTARIO  " _
        & "  					GROUP BY idProducto  " _
        & "  			)QD " _
        & "  			ON QA.id_Producto = QD.idProducto " _
        & "         			LEFT JOIN    " _
        & "         			(   " _
        & "         					SELECT zcorrelativo, idruta  " _
        & "         					FROM [CLIQUIDACION]	          					 " _
        & "         			)QE  " _
        & "         			ON QA.idruta = QE.idRuta "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT


    End Function


    Public Function getMovimientos2(ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "  SELECT id_producto,descripcion,  " _
        & "  case when cantidadInicial is null then 0 else cantidadInicial end as cantidadInicial,  " _
        & "  case when cantidadVendida is null then 0 else cantidadVendida end as cantidadVendida,  " _
        & "  case when cantidadDevolucion is null then 0 else abs(cantidadDevolucion) end as cantidadDevolucion,  " _
        & "  case when cantidadActual is null then 0 else cantidadActual end as cantidadActual,  " _
        & "  unidadesCaja, " & id_glo_codRuta.ToString & " ruta, 0 as diferencia, zcorrelativo," _
        & "  case when (cantidadVendida * litrosUnidad) is null then 0 else (cantidadVendida * litrosUnidad) end as litrosVendido, ttipo " _
        & "  FROM  " _
        & "  ( " _
        & "  	SELECT id_producto, descripcion, cantidad AS cantidadInicial,unidadesCaja, idruta, litrosUnidad, tTipo " _
        & "  	FROM [MOVIMIENTO_INVENTARIO] " _
        & "  		LEFT JOIN RPRODUCTO  " _
        & "  		ON idProducto = id_producto " _
        & "  		WHERE origen = 'CARGA INICIAL' " _
        & "  )QA  " _
        & "  	LEFT JOIN   " _
        & "  	(  " _
        & "  		SELECT  idProducto, abs(sum(cantidad)) as cantidadVendida    " _
        & "  		FROM MOVIMIENTO_INVENTARIO WHERE  origen IN  ('ODV','ANULA-ODV','ZTAP','CD') " _
        & "  		GROUP BY idProducto  " _
        & "  	)QB  " _
        & "  	ON QA.id_Producto = QB.idProducto " _
        & "  		LEFT JOIN   " _
        & "  		(  " _
        & "  				SELECT  idProducto, sum(cantidad) as cantidadDevolucion  " _
        & "  				FROM MOVIMIENTO_INVENTARIO WHERE  origen IN  ('DEVOLUCION')  " _
        & "  				GROUP BY idProducto  " _
        & "  		)QC " _
        & "  		ON QA.id_Producto = QC.idProducto " _
        & "  			LEFT JOIN   " _
        & "  			(  " _
        & "  					SELECT  idProducto, sum(cantidad) as cantidadActual  " _
        & "  					FROM MOVIMIENTO_INVENTARIO  " _
        & "  					GROUP BY idProducto  " _
        & "  			)QD " _
        & "  			ON QA.id_Producto = QD.idProducto " _
        & "         			LEFT JOIN    " _
        & "         			(   " _
        & "         					SELECT zcorrelativo, idruta  " _
        & "         					FROM [CLIQUIDACION]	          					 " _
        & "         			)QE  " _
        & "         			ON QA.idruta = QE.idRuta "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT


    End Function

    'Public Function getInventarioVenta(ByRef rlayer As rLayerHandler) As DataTable
    '    SQL_QUERY = _
    '    " SELECT  " _
    '    + " *, " _
    '    + " case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(Resumen)))end as ACJ, " _
    '    + " case when unidadesCaja = 1 then       convert(nvarchar(100),ceiling(floor(Resumen))) else convert(nvarchar(100), ceiling ((Resumen - floor(Resumen))*unidadesCaja)) end as AUN  " _
    '    + " FROM " _
    '    + " ( " _
    '    + "	SELECT 		 " _
    '    + " 	id_producto, descripcion,[litrosUnidad]*sum(cantidad) litros," _
    '    + "		sum(cantidad/unidadesCaja)cantidadActual, " _
    '    + " 	sum(cantidad)/convert(decimal,unidadesCaja) Resumen, 		 " _
    '    + "		RP.unidadesCaja,  " _
    '    + "     RP.ttipo  ,  " _
    '    + "     NC.idRuta " _
    '    + " FROM" _
    '    + "     [CFACTURA] NC LEFT JOIN " _
    '    + "     [CFACTURA_DETALLE]	 RC " _
    '    + "     on id_EncFactura = idEncFactura " _
    '    + " LEFT JOIN rproducto	RP  " _
    '    + " ON					RP.id_producto = RC.idProducto	 " _
    '    + " LEFT JOIN RCLIENTE ON id_cliente=idCliente " _
    '    + " WHERE NC.eSTADO = 1 " _
    '    + " GROUP BY id_producto, descripcion,RP.unidadesCaja, RP.ttipo,NC.idRuta,litrosUnidad " _
    '    + " )QA order by ttipo asc ,descripcion"
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

   
End Class
