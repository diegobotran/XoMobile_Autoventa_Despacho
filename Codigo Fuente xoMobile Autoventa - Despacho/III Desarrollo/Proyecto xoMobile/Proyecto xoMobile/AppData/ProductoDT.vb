Imports System.Data
Imports Proyecto_xoMobile_Packs
Public Class ProductoDT
    '---ok

    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim objCe As New ceClient

    'Public Function getProductos() As DataTable
    '    SQL_QUERY = " SELECT * FROM RPRODUCTO order by  id_producto asc"
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    Public Function getListadoInventario(ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "	SELECT 	sociedad, idOficinaVentas, idDepartamento, idClaseCliente, idRegion, idCanal, idTipoRuta, convert(nvarchar(10),id_producto) id_producto, convert(nvarchar(10),id_producto) codigo , descripcion, trqt cj_un,  " _
        & "			CASE WHEN B.IDPRODUCTO IS NULL THEN 0 ELSE 1 END AS agregado, " _
        & "			1 as display, 0 as locked, unidadesCaja, 0 as cantidadActual	" _
        & "	FROM  " _
        & "	(  " _
        & "		SELECT S.id_cluster, S.sociedad, S.idOficinaVentas, S.idDepartamento, S.idClaseCliente, S.idRegion, S.idCanal, S.idTipoRuta, S.idProducto, S.tipoDato, S.tipo, S.accion, S.valorMinimo, S.unidadMedida, S.marca, S.desMarca, P.id_producto, P.idEnvase, P.idCaja, P.descripcion, P.categoria, P.litrosUnidad, P.unidadesCaja, P.tTipo, P.idMarca  " _
        & "		FROM [RCLUSTER_SEG] S " _
        & "			LEFT JOIN RPRODUCTO P  ON P.id_producto = S.idproducto " _
        & "		WHERE 	TTIPO = 1  AND tipoDato = 'I' AND S.GIRO = '' " _
        & "	)A  " _
        & "	LEFT JOIN   " _
        & "	(  " _
        & "	SELECT * FROM [CINVENTARIO_FISICO]  " _
        & "	WHERE IDCLIENTE = " & id_glo_cliente _
        & "	)B  " _
        & "	ON A.ID_PRODUCTO  = B.IDPRODUCTO  " _
        & "	ORDER BY ID_PRODUCTO  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getListadoInventarioGiro(ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "	SELECT 	sociedad, idOficinaVentas, idDepartamento, idClaseCliente, idRegion, idCanal, idTipoRuta, convert(nvarchar(10),id_producto) id_producto,convert(nvarchar(10),id_producto) codigo, descripcion, trqt cj_un,  " _
        & "			CASE WHEN B.IDPRODUCTO IS NULL THEN 0 ELSE 1 END AS agregado, " _
        & "			1 as display, 0 as locked, unidadesCaja, 0 as cantidadActual	" _
        & "	FROM  " _
        & "	(  " _
        & "		SELECT S.id_cluster, S.sociedad, S.idOficinaVentas, S.idDepartamento, S.idClaseCliente, S.idRegion, S.idCanal, S.idTipoRuta, S.idProducto, S.tipoDato, S.tipo, S.accion, S.valorMinimo, S.unidadMedida, S.marca, S.desMarca, P.id_producto, P.idEnvase, P.idCaja, P.descripcion, P.categoria, P.litrosUnidad, P.unidadesCaja, P.tTipo, P.idMarca  " _
        & "		FROM [RCLUSTER_SEG] S " _
        & "			LEFT JOIN RPRODUCTO P  ON P.id_producto = S.idproducto inner join rcliente C on id_cliente =  " & id_glo_cliente _
        & "		WHERE 	TTIPO = 1  AND tipoDato = 'I' and S.giro = C.ramo " _
        & "	)A  " _
        & "	LEFT JOIN   " _
        & "	(  " _
        & "	SELECT * FROM [CINVENTARIO_FISICO]  " _
        & "	WHERE IDCLIENTE = " & id_glo_cliente _
        & "	)B  " _
        & "	ON A.ID_PRODUCTO  = B.IDPRODUCTO  " _
        & "	ORDER BY ID_PRODUCTO  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getListadoInventarioDeprecated(ByVal rlayer As rLayerHandler) As DataTable

        SQL_QUERY = " " _
        & "	SELECT CONVERT(NVARCHAR(10), id_producto) as id_producto,CONVERT(NVARCHAR(10), id_producto) as codigo,descripcion, idEnvase,idCaja, CASE WHEN IDPRODUCTO IS NULL THEN 0 ELSE 1 END AS agregado,categoria,litrosUnidad,unidadesCaja,ventaContado,tTipo,''orden ,''Resumen,  1 cantidadInicial , '' cantidadActual ,'' cj_un, 1 as display, 0 as locked   " _
        & "	FROM ( " _
        & "	SELECT * FROM RPRODUCTO " _
        & "	WHERE 	TTIPO = 1  " _
        & "	)A " _
        & "	LEFT JOIN  " _
        & "	( " _
        & "	SELECT * FROM [CINVENTARIO_FISICO] " _
        & "	WHERE IDCLIENTE = " & id_glo_cliente _
        & "	)B " _
        & "	ON A.ID_PRODUCTO  = B.IDPRODUCTO " _
        & "	ORDER BY ID_PRODUCTO "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT

    End Function

    Public Function getListadoEnvases() As DataTable
        SQL_QUERY = " SELECT  CONVERT(NVARCHAR(10), id_producto) as codigo, descripcion, idEnvase,idCaja, 0 agregado,categoria,litrosUnidad,unidadesCaja,ventaContado,tTipo,''orden ,''Resumen,  1 cantidadInicial , '' cantidadActual ,'' cj_un , 1 as display, 0 as locked FROM RPRODUCTO  WHERE TTIPO = 1 and ( idEnvase  <> 0  or idCaja <> 0 ) order by idEnvase desc  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Public Function getListadoVenta(ByVal rlayer As rLayerHandler) As DataTable
    '    SQL_QUERY = " " _
    '    + "	SELECT *,CASE WHEN unidadesCaja = 1 THEN '0/'+ convert(nvarchar(100),ceiling(floor(resumen))) ELSE convert(nvarchar(100),ceiling(floor(resumen)))+'/'+ convert(nvarchar(100), ceiling ((resumen - floor(resumen))*unidadesCaja)) END AS cj_un" _
    '    + "	FROM " _
    '    + "	( " _
    '    + "		SELECT   CONVERT(NVARCHAR(25), id_producto) as codigo,descripcion, idEnvase,idCaja, 0 agregado, categoria,litrosUnidad,unidadesCaja,ventaContado,tTipo,'' orden, cantidadActual/convert(decimal,unidadesCaja) Resumen  , cantidadInicial , cantidadActual " _
    '    + "		FROM 	RPRODUCTO LEFT JOIN rcarga  " _
    '    + "		ON id_producto = idproducto  " _
    '    + "		WHERE idruta = " + id_glo_ruta.ToString() + " AND ttipo  = 1  " _
    '    + "		AND cantidadActual > 0	 " _
    '    + "	)QA " _
    '    + "	order by  codigo asc  "
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
    '    Return SQL_DT
    'End Function

    Public Function getListadoDespacho(ByVal idDespacho As String, ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "	 SELECT  FD.*, convert(nvarchar(10),FD.idproducto) as id_producto, P.descripcion, FD.trqt as cj_un, 1 agregado  , 	 FD.importeDesto as descuento, FD.isDiferente, D.tipoPago, 0 display, 0 locked " _
        & "	 FROM [DESPACHO]  D           " _
        & "	 left join CFACTURA on nopedido = D.serie " _
        & "	 LEFT JOIN CFACTURA_DETALLE FD   on id_encFactura = idEncFactura " _
        & "	 LEFT JOIN RPRODUCTO P ON id_producto = idProducto " _
        & "	 where CFACTURA.ID_PEDIDO =  " & idDespacho _
        & "  Order By item"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getListadoCambio(ByVal idDespacho As String, ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "	 SELECT  FD.id_detFactura, FD.idEncFactura, FD.item, FD.idProducto, FD.um, FD.cantidad, 0 precio, 0 importeSinIva, 0 importe,Fd.iva, FD.porcentajeDesto, FD.porcentajeDestoPP, FD.importeDestoPP, Fd.importeDesto, FD.tipoVenta, FD.idRubro, FD.trqt, FD.litm, FD.estado, FD.valorIvaDesto, FD.valorIvaImporte, FD.id_pedido, FD.posicionSuperior, FD.noPedido, FD.litros, FD.isDiferente, FD.canDespacho, convert(nvarchar(10),FD.idproducto) as id_producto, P.descripcion, FD.trqt as cj_un, 1 agregado  , 	 FD.importeDesto as descuento, FD.isDiferente, D.tipoPago, 0 display, 0 locked " _
        & "	 FROM [DESPACHO]  D           " _
        & "	 left join CFACTURA on nopedido = D.serie " _
        & "	 LEFT JOIN CFACTURA_DETALLE FD   on id_encFactura = idEncFactura " _
        & "	 LEFT JOIN RPRODUCTO P ON id_producto = idProducto " _
        & "	 where CFACTURA.ID_PEDIDO =  " & idDespacho _
        & "  Order By item"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    'Public Function getListadoVenta(ByVal rlayer As rLayerHandler) As DataTable
    'SQL_QUERY = " " _
    '& " SELECT   " _
    '& "         	convert(nvarchar(25),id_producto) + '' as idproducto,descripcion,       " _
    '& "         	case when cantidadActual is null then 0 else cantidadActual end as cantidadActual,    " _
    '& "         	case when cantidadInicial is null then 0 else cantidadInicial end as cantidadInicial,   " _
    '& "         	unidadesCaja,0 agregado	, 1 AS display  " _
    '& "         FROM    " _
    '& "          (  " _
    '& "          	SELECT id_producto, descripcion, cantidad AS cantidadInicial,unidadesCaja, ttipo  " _
    '& "          	FROM [MOVIMIENTO_INVENTARIO]  " _
    '& "          		LEFT JOIN RPRODUCTO   " _
    '& "          		ON idProducto = id_producto  " _
    '& "          		WHERE origen = 'CARGA INICIAL'  " _
    '& "          )QA            " _
    '& "         LEFT JOIN  " _
    '& "         (  " _
    '& "         	SELECT  idProducto, sum(cantidad) as cantidadActual    " _
    '& "         	FROM MOVIMIENTO_INVENTARIO    " _
    '& "         	GROUP BY idProducto    " _
    '& "         )QD   " _
    '& "         ON id_Producto = QD.idProducto  " _
    '& "         where cantidadactual > 0 and ttipo = 1"

    '    SQL_QUERY = " " _
    '    & "	SELECT  CONVERT(NVARCHAR(25), id_producto) + '' as id_producto,descripcion, idEnvase,idCaja, 0 agregado,categoria,litrosUnidad,unidadesCaja,ventaContado,tTipo,''orden ,''Resumen,  1 cantidadInicial , '' cantidadActual ,'' cj_un, 0 as locked, 1 as display" _
    '    & "	FROM RPRODUCTO WHERE TTIPO = 1  "

    '    SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
    '    Return SQL_DT
    'End Function

    Public Function getListadoVenta(ByVal rlayer As rLayerHandler) As DataTable
        'SQL_QUERY = " " _
        '     & "	SELECT  CONVERT(NVARCHAR(25), id_producto) + '' as codigo,descripcion, idEnvase,idCaja, 0 agregado,categoria,litrosUnidad,unidadesCaja,ventaContado,tTipo,''orden ,''Resumen,  1 cantidadInicial , '' cantidadActual ,'' cj_un, 0 as locked, 1 as display" _
        '    & "	FROM RPRODUCTO WHERE TTIPO = 1  "
        '       SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        '      Return SQL_DT

        SQL_QUERY = " " _
       & " SELECT   " _
         & "         	convert(nvarchar(25),id_producto) + '' as codigo, convert(nvarchar(10),id_producto) id_producto, descripcion,       " _
                 & "         	case when cantidadActual is null then 0 else cantidadActual end as cantidadActual,    " _
                 & "         	case when cantidadInicial is null then 0 else cantidadInicial end as cantidadInicial,   " _
                 & "         	unidadesCaja,0 agregado	,0 as locked, 1 AS display  " _
                & "         FROM    " _
               & "          (  " _
              & "          	SELECT id_producto, descripcion, cantidad AS cantidadInicial,unidadesCaja, ttipo  " _
             & "          	FROM [MOVIMIENTO_INVENTARIO]  " _
            & "          		LEFT JOIN RPRODUCTO   " _
           & "          		ON idProducto = id_producto  " _
                 & "          		WHERE origen = 'CARGA INICIAL'  " _
                & "          )QA            " _
               & "         LEFT JOIN  " _
              & "         (  " _
             & "         	SELECT  idProducto, sum(cantidad) as cantidadActual    " _
            & "         	FROM MOVIMIENTO_INVENTARIO    " _
           & "         	GROUP BY idProducto    " _
          & "         )QD   " _
         & "         ON id_Producto = QD.idProducto  " _
                 & "         where cantidadactual > 0 and ttipo = 1"

        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function


    Public Function getLocks(ByVal rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & "	SELECT case when L.IDPRODUCTO is null then 0 else L.idProducto end as idProducto, L.SOCIEDAD, L.REGION,L.GRUPOVENTAS,L.RUTA,L.RAMO,L.CATEGORIA,L.IDCLIENTE,L.RAMO5, C.TIPODI " _
        & "	FROM RCLIENTE C	    	   " _
        & "	JOIN RLOCK_MATERIAL L  " _
        & "	ON C.ramo = L.ramo " _
        & "	OR C.categoria = L.categoria	" _
        & "	OR C.id_cliente = L.idcliente  " _
        & "	OR C.sociedad = L.sociedad " _
        & "	OR C.region = L.region  " _
        & "	OR C.grupoVentas = L.grupoVentas  " _
        & "	OR C.ruta = L.ruta " _
        & "	OR C.ramo5 = L.ramo5 " _
        & "	WHERE ID_CLIENTE = " & id_glo_cliente _
        & "	"

        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function



    Public Function getDetalleZsearch(ByVal idProducto As String)
        SQL_QUERY = " SELECT CONVERT (NVARCHAR(25),id_producto)id_producto, categoria categoriaP " _
        + " FROM RPRODUCTO " _
        + " WHERE id_producto =  " + idProducto
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDetalle(ByVal idProducto As String)
        SQL_QUERY = " " _
        & " SELECT   " _
        & " 	P.*, " _
        & "  	CASE  when QA.cantidadactual IS NULL THEN 0 ELSE  QA.cantidadactual END cantidadActual,  " _
        & "        CASE  when QB.cantidadactual IS NULL THEN 0 ELSE  QB.cantidadactual END cantidadActualE ,  " _
        & "        CASE  when QC.cantidadactual IS NULL THEN 0 ELSE  QC.cantidadactual END cantidadActualC   " _
        & " FROM RPRODUCTO P " _
        & " LEFT JOIN  " _
        & " ( " _
        & " SELECT   idProducto, sum(cantidad) as cantidadActual " _
        & " FROM MOVIMIENTO_INVENTARIO   " _
        & " GROUP BY idProducto   " _
        & " )QA " _
        & " ON QA.idProducto = P.id_producto " _
        & " LEFT JOIN  " _
        & " ( " _
        & " SELECT   idProducto, sum(cantidad) as cantidadActual " _
        & " FROM MOVIMIENTO_INVENTARIO   " _
        & " GROUP BY idProducto   " _
        & " )QB " _
        & " ON QB.idProducto = P.idEnvase " _
        & " LEFT JOIN  " _
        & " ( " _
        & " SELECT   idProducto, sum(cantidad) as cantidadActual " _
        & " FROM MOVIMIENTO_INVENTARIO   " _
        & " GROUP BY idProducto   " _
        & " )QC " _
        & " ON QC.idProducto = P.idCaja " _
        & " WHERE P.id_producto = " & idProducto
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getSecuenciaAcceso(ByVal ttipo As String) As DataTable
        SQL_QUERY = "select distinct  secuencia as acc, tabla from zcampos2 where condicion = '" + ttipo + "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getZcampos(ByVal ttipo As String) As DataTable
        'SQL_QUERY = " SELECT * FROM zcampos2 WHERE condicion = '" + ttipo + "' " + " UNION SELECT * FROM zcampos2 WHERE condicion = 'ENVASE'"
        SQL_QUERY = " SELECT secuencia, tabla, id_cliente, id_producto, importe,factor, categoriap, categoriac,um  FROM zcampos2 WHERE condicion = '" + ttipo + "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function bulkCajasUnidades() As Boolean

        Dim insertados, borrados As Integer


        SQL_QUERY = " " _
        + " INSERT 	INTO [RCARGA_LOG]( [sd] , [idRuta],[idProducto],[um],[cantidadInicial],[cantidadActual],[control],[fechaCreacion], [horaCreacion],[confirmada]) " _
        + " ( " _
        + " SELECT 	[sd], idRuta,id_producto, 'UN' um,cantidadActual * RP.unidadesCaja, cantidadActual*RP.unidadesCaja,1 as control ,[fechaCreacion], [horaCreacion],[confirmada] " _
             + " FROM 	RCARGA_LOG		RC " _
             + " LEFT JOIN rproducto	RP  " _
             + " ON		RP.id_producto = RC.idProducto	 " _
             + " WHERE 	um	=	'CJ' and confirmada = 'false' " _
        + " ) "
        objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = " " _
        + " DELETE  FROM  RCARGA_LOG " _
        + " WHERE um = 'CJ' "
        objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = " " _
        + " INSERT 	INTO [RCARGA_LOG]( [sd] , [idRuta],[idProducto],[um],[cantidadInicial],[cantidadActual],[control],[fechaCreacion], [horaCreacion], [confirmada]) " _
        + " ( " _
        + "     select sd, idRuta, idProducto, um, sum(cantidadinicial)cantidadInicial, sum(cantidadActual)cantidadActual, 0 control,[fechaCreacion], [horaCreacion] ,[confirmada]" _
        + "     from RCARGA_LOG  where confirmada = 'false'" _
        + "     group by  sd, idRuta, idProducto, um,control, fechaCreacion, horaCreacion, confirmada " _
        + " ) "
        insertados = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = " " _
        + " DELETE  FROM  RCARGA_LOG " _
        + " WHERE control  = 1 "
        objCe.SetExecute(SQL_QUERY)


        SQL_QUERY = " DELETE FROM RCARGA_LOG WHERE confirmada = 'false' and sd in (SELECT DISTINCT SD  FROM RCARGA_LOG WHERE confirmada = 'true' ) "
        borrados = objCe.SetExecute(SQL_QUERY)

        '--- Revisa que el numero de insertados sea diferente al numero de borrados para dar ok
        If borrados = insertados Then Return False Else Return True

    End Function

    Public Function bulkBoom(ByVal ttipo As String) As Boolean
        SQL_QUERY = " " _
        + "	select  id_producto, idProductoBoom	" _
        + "	from	" _
        + "	(	 	" _
        + "		select id_producto, idExplosion from rproducto 	" _
        + "		left join boom on id_producto = idproducto	" _
        + "		where id_boom is not null	" _
        + "	)QA	" _
        + "	Left Join 	" _
        + "	(	" _
        + "		select distinct id_producto idProductoBoom , ttipo from boom	" _
        + "		left join rproducto on idExplosion = id_producto	" _
        + "		where id_producto is not null and ttipo = 	" + ttipo _
        + "	)QB	" _
        + "	ON	QA.idExplosion = QB.idProductoBoom	" _
        + "	WHERE TTIPO = 	" + ttipo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)

        For i = 0 To SQL_DT.Rows.Count - 1
            If ttipo = 2 Then
                SQL_QUERY = " " _
                + " UPDATE RPRODUCTO SET idEnvase = " + SQL_DT.Rows(i).Item("idProductoBoom").ToString _
                + " WHERE id_producto = " + SQL_DT.Rows(i).Item("id_Producto").ToString
                objCe.SetExecute(SQL_QUERY)
            Else
                SQL_QUERY = " " _
                + " UPDATE RPRODUCTO SET idCaja = " + SQL_DT.Rows(i).Item("idProductoBoom").ToString _
                + " WHERE id_producto = " + SQL_DT.Rows(i).Item("id_Producto").ToString
                objCe.SetExecute(SQL_QUERY)
            End If
        Next
        Return True
    End Function

    Public Function getDescuentoManual(ByVal idProducto As String, ByRef rLayer As rLayerHandler) As DataTable
        SQL_QUERY = "SELECT * FROM ZCAMPOS2 WHERE ISMANUAL ='M' and id_producto like '%" + idProducto + "%'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rLayer)
        Return SQL_DT
    End Function

    Public Function getPrecio(ByVal ttipo As String, ByVal idListaPrecio As String, ByVal idProducto As String, ByVal rLayer As rLayerHandler)
        SQL_QUERY = "" & _
        " SELECT  * FROM  ZCAMPOS2" & _
        " WHERE listaPrecios = '" & Trim(idListaPrecio) & "'" & _
        " AND   id_producto like '%" & idProducto & "%'" & _
        " AND condicion = '" & Trim(ttipo) & "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rLayer)
        Return SQL_DT
    End Function

    Public Function getDescuento(ByVal condicion As String, ByVal cliente As ClienteCO, ByVal categoriaP As String, ByVal rLayer As rLayerHandler)
        SQL_QUERY = "" & _
        " SELECT  * FROM  ZCAMPOS2 " & _
        " WHERE condicion LIKE '" & Trim(condicion) & "'" & _
        " AND categoriaC LIKE '" & Trim(cliente.categoria) & "'" & _
        " AND categoriaP LIKE '" & Trim(categoriaP) & "'" & _
        " ORDER BY importe"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rLayer)
        Return SQL_DT
    End Function

    '---xoMobile 2.0

    Public Function getBom(ByVal idProducto As String, ByVal rLayer As rLayerHandler)
        SQL_QUERY = "" & _
        " SELECT  idproducto, convert(int,idexplosion)idexplosion FROM  boom" & _
        " WHERE idProducto  = " & Trim(idProducto)
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rLayer)
        Return SQL_DT
    End Function
End Class
