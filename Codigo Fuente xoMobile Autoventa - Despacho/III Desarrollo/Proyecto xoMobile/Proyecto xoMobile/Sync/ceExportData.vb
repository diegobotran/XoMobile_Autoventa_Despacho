Imports System.Data
Imports System.Data.SqlServerCe

Public Class ceExportData
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim objCe As New ceClient
    Dim dtEncuesta As New DataTable

#Region " EXPORTAR PREGUNTAS INICIALES / FINALES "
    Public Function agregarPreguntasIF()
        SQL_QUERY = " SELECT   id_salidaIngreso, respuesta, fechaOperaciON," & id_glo_codRuta & "idRuta FROM [RSALIDA_INGRESO] "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

#End Region

#Region " EXPORTAR INVENTARIO FISICO DEL CLIENTE "
    Public Function agregarInventarioFisico()
        SQL_QUERY = " " _
        & " SELECT   	fechaEmisiON, " & id_glo_codRuta & " idRuta ,[idCliente],[idProducto],[um],sum(cantidad)[cantidad],sum(importeSinIva)importeSinIva " _
        & " FROM " _
        & " (	SELECT   	cONvert(nvarchar(10),[fechaEmisiON],103)fechaEmisiON,20 idRuta ,[idCliente],[idProducto],[um],cantidad,importeSinIva " _
        & " 	FROM 		CINVENTARIO_FISICO	)A " _
        & " GROUP BY 	fechaEmisiON, [idCliente],[um], idproducto "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR CORRELATIVO SAP "
    Public Function agregarCorrelativoSAP()
        SQL_QUERY = " SELECT   " & id_glo_codRuta & " idRuta , [dTipo],[noResoluciON],[fechaResoluciON],[serie],[inicial], actual-1 actual,[final],[usuarioCreaciON],[fechaCreaciON] FROM RCCORRELATIVO"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

#End Region

#Region " EXPORTAR ACTUALIZACIÓN CLIENTES NIT-CUI-PASAPORTE "
    Public Function actualizarclienteSAP()
        SQL_QUERY = " SELECT   " & id_glo_sociedad & " idsociedad ," & id_glo_codRuta & " idruta , [id_cliente],[nit],[numeroDi] FROM RCLIENTE WHERE actualiza_cui = 'X' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

#End Region

#Region " EXPORTAR ACTUALIZACIÓN CLIENTES CON NOMBRE FEL "
    Public Function actualizaNombreFEL()
        SQL_QUERY = " SELECT   " & id_glo_sociedad & " idsociedad ," & id_glo_codRuta & " idruta , [idcliente] id_cliente,[nombre_fel], [fechaEmision] fecha, RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) as documento, serie, preimpreso dte, numeroautorizacion uuid, tipo_receptor tipor, idreceptor idrecep FROM cfactura WHERE idReceptor <> 'C/F' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR ACTUALIZACIÓN LIBRO DE VENTAS"
    Public Function actualizalibroventas()
        SQL_QUERY = " SELECT   " & id_glo_sociedad & " idsociedad ," & id_glo_codRuta & " idruta , [idcliente] id_cliente,[nombre_fel], [fechaEmision] fecha, RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) as documento, serie, preimpreso dte, numeroautorizacion uuid, tipo_receptor tipor, idreceptor idrecep FROM cfactura WHERE estado = 1 " _
                    & " UNION ALL " _
                    & "  SELECT   " & id_glo_sociedad & " idsociedad ," & id_glo_codRuta & " idruta , [idcliente] id_cliente,[nombre_fel], [fechaEmision] fecha, RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) as documento, serie, preimpreso dte,numeroautorizacion uuid, tipo_receptor tipor, idreceptor idrecep FROM cnotacredito WHERE estado = 1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR CORRELATIVO CONTINGENCIA "
    Public Function agregarCorrelativoSAPContigencia()
        SQL_QUERY = " SELECT   [sociedad], REPLACE ( CONVERT( nvarchar(15), fecha_sat, 111 ) , '/' , '' ) fecha_sat, " & id_glo_codRuta & " idRuta , [numeroAcceso],[codigo_cliente],[serie],[preimpreso],[referencia], [usado], [registrado], " & id_glo_usuario & " idUsuario, REPLACE ( CONVERT( nvarchar(15), fecha, 111 ) , '/' , '' ) fecha, [hora]   FROM RCONTINGENCIA WHERE usado = 'X'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR ENCUESTAS "
    Public Function agregarEncuestaRespuestas()
        SQL_QUERY = "   SELECT  	 " & id_glo_codRuta & " idRuta ,idCliente, D.idEncuesta,idTema,idPregunta, E.fecha fechaEncuesta, D.fecha fechaPregunta, D.linea, respuesta,idUsuario " _
         & " FROM 	    cencuesta E  left join cencuesta_detalle D " _
         & " ON 		idEncEncuesta = id_cEncuesta "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR BITACORA "
    Public Function agregarBitacora()
        SQL_QUERY = " SELECT   id_bitacora, " & id_glo_codRuta & " idRuta, idCliente, idUsuario,tOperaciON,fechaEmisiON, latitud, longitud  FROM CBITACORA"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD "
    Public Function agregarEfectividad(ByVal ruta As String)

        SQL_QUERY = " " _
       & " SELECT  id_cliente clientes, max(visitado) as visitado, " _
        & " CASE WHEN diaVisitado = 1 then 7 WHEN diaVisitado = 2 then 1	WHEN diaVisitado = 3 then 2	WHEN diaVisitado = 4 then 3	WHEN diaVisitado = 5 then 4	WHEN diaVisitado = 6 then 5	WHEN diaVisitado = 7 then 6 end diaVisita, " _
        & " CASE WHEN diaActual = 1 then 7 WHEN diaActual = 2 then 1	WHEN diaActual = 3 then 2	WHEN diaActual = 4 then 3	WHEN diaActual = 5 then 4	WHEN diaActual = 6 then 5	WHEN diaActual = 7 then 6 end diaActual, " _
        & " fechaEmision Fecha, '" & ruta & "' Ruta, sum(fimporte)ValorVenta,sum(rimporte)ValorCobro,sum(limporte)ValorInventario, " _
        & " CASE WHEN diavisita <> CASE WHEN diaVisitado = 1 then 7 WHEN diaVisitado = 2 then 1	WHEN diaVisitado = 3 then 2	WHEN diaVisitado = 4 then 3	WHEN diaVisitado = 5 then 4	WHEN diaVisitado = 6 then 5	WHEN diaVisitado = 7 then 6 end then 'X'  WHEN visitado = 0 then  'D' else 'V' end as FlagVisita ,razonNovisita,razonNoventa, orden  " _
        & " FROM " _
        & " 	( 	" _
        & " 	SELECT  " _
        & " 		CASE WHEN B.idCliente is null then A.id_cliente else B.idCliente end as id_cliente, " _
        & " 		A.visitado  as visitado, " _
        & " 		CASE WHEN B.diaVisitado is null then A.diaVisitado else B.diaVisitado end as diaVisitado, " _
        & " 		CASE WHEN B.diaActual is null then A.diaActual  else B.diaActual end as diaActual, " _
        & " 		CASE WHEN B.fechaEmision is null then A.fechaEmision else B.fechaEmision end as fechaEmision, " _
        & " 		CASE WHEN B.fimporte is null then A.fimporte else B.fimporte end as fimporte, " _
        & " 		CASE WHEN B.rimporte is null then A.rimporte else B.rimporte end as rimporte, " _
        & " 		CASE WHEN B.limporte is null then A.limporte else B.limporte end as limporte, " _
        & " 		A.razonNovisita, A.razonNoventa , diavisita, orden" _
        & " 		FROM  " _
        & " 		( " _
        & " 		SELECT  id_cliente,visitado,datepart(dw,getdate())diaVisitado, datepart(dw,getdate())diaActual, convert(nvarchar(10),getdate(),103) as fechaEmision, 0 fimporte,0 rimporte,0 limporte,	razonNovisita,razonNoventa, diaVisita, orden " _
        & " 		FROM RCLIENTE 		 " _
        & " 	)A " _
        & " 	LEFT  JOIN " _
        & " 	( " _
        & " 	 	SELECT   idcliente,diavisitado,diaActual, fechaEmision, SUM(fimporte)fimporte,sum(rimporte)rimporte,sum(limporte) limporte  " _
        & " 	          	FROM  " _
        & " 	          	(  " _
        & " 	          		SELECT   idcliente, datepart(dw,fechaEmision)diaVisitado, datepart(dw,getdate()) diaActual,convert(nvarchar(10),fechaEmision,103) as fechaEmision,F.importe FIMPORTE,0 as RIMPORTE,0 as LIMPORTE  " _
        & " 	          		FROM cfactura F  " _
        & " 	          		union   " _
        & " 	          		SELECT   idcliente, datepart(dw,fechaEmision)diaVisitado, datepart(dw,getdate()) diaActual,convert(nvarchar(10),fechaEmision,103) as fechaEmision, 0 as importe, R.importe,0 as Iimporte  " _
        & " 	          		FROM cRecibo R  " _
        & " 	          		union  " _
        & " 	          		SELECT   idcliente, datepart(dw,fechaEmision)diaVisitado, datepart(dw,getdate()) diaActual,convert(nvarchar(10),fechaEmision,103) as fechaEmision, 0 as Fimporte, 0 AS rimporte,I.importe  " _
        & " 	          		FROM  cinventario_fisico I  " _
        & " 	          	)A  " _
        & " 	          	group by idcliente,diavisitado,diaActual, fechaEmision  " _
        & " 	)B " _
        & " 	ON A.ID_CLIENTE = B.IDCLIENTE " _
        & " 	WHERE (DIAVISITA =  CASE WHEN datepart(dw,getdate()) = 1 then 7 WHEN datepart(dw,getdate()) = 2 then 1	WHEN datepart(dw,getdate()) = 3 then 2	WHEN datepart(dw,getdate()) = 4 then 3	WHEN datepart(dw,getdate()) = 5 then 4	WHEN datepart(dw,getdate()) = 6 then 5	WHEN datepart(dw,getdate()) = 7 then 6 end ) or (visitado <> 0) " _
        & " )QA  " _
        & " group by id_cliente,visitado,diavisitado,diaActual, fechaEmision, razonNovisita,razonNoventa, diavisita, orden " _
        & " order by orden "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR A BAPI MAESTRA "

    Public Function xo_get_NotaCreditoDE_Encabezado()
        SQL_QUERY = " " _
            & "  SELECT      CASE WHEN NC.estado = 2 then 9 else 1 end as Cabecera, ncnumero,'DE' TipoDocumento, NC.serie, NC.numeroautorizacion, NC.preimpreso, nc.idCliente, REPLACE ( CONVERT( nvarchar(15), nc.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc,RTRIM(cxc.serie)+CONVERT(NVARCHAR(50),cxc.numero) documentoAfecta, " & id_glo_centro & "  CentroL , cONdiciON AS codigoPago, nc.estado ,NC.importe,CASE WHEN NC.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, NC.tipo_receptor, NC.idreceptor " _
            & "  FROM 		cnotacredito NC   " _
            & "  LEFT JOIN 	CRECIBO R	ON NC.idEncRecibo = id_encRecibo   " _
            & "  LEFT JOIN	RCXC CXC	ON R.idEncCxc = id_cxc   " _
            & "  WHERE  	NC.ttipo = 4 " _
            & "  AND 	NC.tipoDoc is null " _
            & "  AND		cxc.numero is not null  " _
            & "  UNION  " _
            & "  SELECT  	CASE WHEN NC.estado = 2 then 9 else 1 end as Cabecera, ncnumero ,'DE' TipoDocumento, nc.serie, nc.numeroautorizacion, nc.preimpreso, nc.idCliente, REPLACE ( CONVERT( nvarchar(15), nc.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc,RTRIM(f.fserie)+CONVERT(NVARCHAR(50),f.fnumero) documentoAfecta,   " & id_glo_centro & "  CentroL ,NC.cONdiciON AS codigoPago, nc.estado ,NC.importe,CASE WHEN NC.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, NC.tipo_receptor, NC.idreceptor " _
            & "  FROM		cnotacredito NC   " _
            & "  LEFT JOIN 	CFACTURA  F	ON NC.idEncFactura = id_encFactura           " _
            & "  WHERE  	NC.ttipo = 4 " _
            & "  AND 	NC.tipoDoc is null " _
            & "  AND 		f.fnumero is not null  "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaAbono_Encabezado()
        SQL_QUERY = " " _
            & "  SELECT      CASE WHEN NC.estado = 2 then 9 else 1 end as Cabecera, ncnumero,'NA' TipoDocumento, NC.serie, NC.numeroautorizacion, NC.preimpreso, nc.idCliente, REPLACE ( CONVERT( nvarchar(15), nc.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc,RTRIM(cxc.serie)+CONVERT(NVARCHAR(50),cxc.numero) documentoAfecta, " & id_glo_centro & "  CentroL , cONdiciON AS codigoPago, nc.estado ,NC.importe,CASE WHEN NC.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, NC.tipo_receptor, NC.idreceptor " _
            & "  FROM 		cnotacredito NC   " _
            & "  LEFT JOIN 	CRECIBO R	ON NC.idEncRecibo = id_encRecibo   " _
            & "  LEFT JOIN	RCXC CXC	ON R.idEncCxc = id_cxc   " _
            & "  WHERE  	NC.ttipo = 4  and tipodoc = 'NA' " _
            & "  AND		cxc.numero is not null  " _
            & "  UNION  " _
            & "  SELECT  	CASE WHEN NC.estado = 2 then 9 else 1 end as Cabecera, ncnumero ,'NA' TipoDocumento, NC.serie, NC.numeroautorizacion, NC.preimpreso, nc.idCliente, REPLACE ( CONVERT( nvarchar(15), nc.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc,RTRIM(f.fserie)+CONVERT(NVARCHAR(50),f.fnumero) documentoAfecta,   " & id_glo_centro & "  CentroL ,NC.cONdiciON AS codigoPago, nc.estado ,NC.importe,CASE WHEN NC.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, NC.tipo_receptor, NC.idreceptor " _
            & "  FROM		cnotacredito NC   " _
            & "  LEFT JOIN 	CFACTURA  F	ON NC.idEncFactura = id_encFactura           " _
            & "  WHERE  	NC.ttipo = 4  and tipodoc = 'NA' " _
            & "  AND 		f.fnumero is not null  "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaCreditoDE_Articulo(ByVal nONotaCredito As String)
        SQL_QUERY = "" _
       & " SELECT  		2 Cabecera,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc, d.idProducto,sum(cantidad) cantidad,'ST' UM " _
       & " FROM 		cnotacredito NC " _
       & " LEFT JOIN 	cnotacredito_detalle D ON id_encnc = idEncNc " _
       & " WHERE  		NC.ttipo = 4 " _
       & " AND 		ncNumero =  " & nONotaCredito _
       & " group by  ncserie,ncnumero,idproducto "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_factura_Encabezado()
        '        SQL_QUERY = "SELECT   " _
        '                   & " cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, numerofactura,idusuario,codigopago,estado,sum(importe)importe,des_estado, " & id_glo_centro & "  centroL, id_pedido" _
        '                  & " FROM " _
        '                 & " ( " _
        '                & "	SELECT   " _
        '               & "	CASE WHEN f.estado = 2 then 9 else 1 end as Cabecera, 	f.fnumero, 'FC' TipoDocumento,	f.idCliente, 	REPLACE ( CONVERT( nvarchar(15), f.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, 	RTRIM(f.fserie)+CONVERT(NVARCHAR(50),f.fnumero) NumeroFactura,	f.idusuario,	f.cONdiciON AS codigoPago,	f.estado,   	d.importe,	CASE WHEN f.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, F.id_pedido	 " _
        '              & "	FROM cfactura f left join cfactura_detalle d ON id_encfactura = idencfactura " _
        '             & " WHERE ttipo ='ODV ' )a " _
        '            & " group by cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, numerofactura,idusuario,codigopago,estado,des_estado, id_pedido "

        SQL_QUERY = "SELECT   " _
                    & " cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, numerofactura,idusuario,codigopago, estado, sum(importe)importe,des_estado, serie, numeroautorizacion, preimpreso, " & id_glo_centro & "  centroL, id_pedido, tipo_receptor, idreceptor" _
                    & " FROM " _
                    & " ( " _
                    & "	SELECT   " _
                    & "	CASE WHEN f.estado = 2 then 9 else 1 end as Cabecera, 	f.fnumero, 'FC' TipoDocumento,	f.idCliente, 	REPLACE ( CONVERT( nvarchar(15), f.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, 	RTRIM(f.fserie)+CONVERT(NVARCHAR(50),f.fnumero) NumeroFactura,	f.idusuario,	f.cONdiciON AS codigoPago,	f.estado,   	d.importe,	CASE WHEN f.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, substring(F.id_pedido,1,1) + '0' + substring(F.id_pedido,2,11) AS id_pedido, f.serie, f.numeroautorizacion, f.preimpreso, f.tipo_receptor, f.idreceptor	 " _
                    & "	FROM cfactura f left join cfactura_detalle d ON id_encfactura = idencfactura " _
                    & " WHERE ttipo ='ODV ' )a " _
                    & " group by cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, numerofactura,idusuario,codigopago,estado,des_estado, id_pedido, serie, numeroautorizacion, preimpreso, tipo_receptor, idreceptor "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_factura_EncabezadoFel(ByVal NoFactura As String)
        SQL_QUERY = "" _
       + " select  8 Cabecera , serie, numeroautorizacion,  preimpreso, RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura " _
       + " from 	cfactura " _
       + " where 	fnumero  =  " + NoFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function xo_get_notacredito_EncabezadoFel(ByVal NoDocumento As String)
        SQL_QUERY = "" _
       + " select  8 Cabecera , serie, numeroautorizacion,  preimpreso, RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncNumero) NumeroFactura " _
       + " from 	cnotacredito " _
       + " where 	ncNumero  =  " + NoDocumento
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_cambio_Encabezado()
        Dim sigla As String

        sigla = " 'CD-'+"
        
        SQL_QUERY = "" _
        & " SELECT   DISTINCT 1 Cabecera ,'CD'tipoDocumento, R.codruta,REPLACE ( CONVERT( nvarchar(15), C.fechaEmision, 111 ) , '/' , '' ) fechaEmisiON," & sigla & "CONVERT( nvarchar(9), C.fechaEmision, 112 ) + REPLACE (CONVERT( nvarchar(5), C.fNumero, 8  ),':','') +CONVERT(NVARCHAR(5),R.codruta) fnumero," & id_glo_usuario.ToString & " as idvendedor,'IL01'CodigoPago,  " & id_glo_centro & " centroL, C.tMotivo motivo, C.id_encFactura documento " _
        & " FROM [CFACTURA] C" _
        & " inner join rruta R ON R.id_ruta = C.idruta " _
        & " WHERE C.TTIPO = 'CD' AND estado = 1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_despacho_no_Facturado()
        SQL_QUERY = " " _
                   & " select 'X' cabecera, 'MARCAR' accion , F.noEntrega " _
                   & " from Despacho D " _
                   & " left join cfactura F on  F.id_pedido = D.idPedido " _
                   & " where F.ttipo = 'PED' and (despachado is null or despachado = 'False') "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_factura_Encabezado(ByVal faltantes As Boolean)
        SQL_QUERY = "SELECT   " _
                    & " cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, serie, numeroautorizacion, preimpreso, numerofactura,idusuario,codigopago,estado,sum(importe)importe,des_estado, " & id_glo_centro & "  centroL, tipo_receptor, idreceptor " _
                    & " FROM " _
                    & " ( " _
                    & "	SELECT   " _
                    & "	CASE WHEN f.estado = 2 then 9 else 1 end as Cabecera, 	f.fnumero, 'FV' TipoDocumento, f.serie, f.numeroautorizacion, f.preimpreso,	f.idCliente, 	REPLACE ( CONVERT( nvarchar(15), f.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, 	RTRIM(f.fserie)+CONVERT(NVARCHAR(50),f.fnumero) NumeroFactura,	f.idusuario,	f.cONdiciON AS codigoPago,	f.estado,   	d.importe,	CASE WHEN f.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, f.tipo_receptor, f.idreceptor	 " _
                    & "	FROM cfactura f left join cfactura_detalle d ON id_encfactura = idencfactura " _
                    & " WHERE ttipo not in  ('ODV','PED','CD') )a " _
                    & " group by cabecera, fnumero, tipodocumento, idcliente, fechaemisiON, numerofactura,idusuario,codigopago,estado,des_estado, serie, numeroautorizacion, preimpreso, tipo_receptor, idreceptor "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_factura_Articulo(ByVal NoFactura As String)
        SQL_QUERY = "" _
        & " SELECT  	2 Cabecera ,RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura,SUM(item),idproducto,SUM(cantidad) cantidad,'ST' um    " _
        & " FROM 	cfactura left join cfactura_detalle ON id_encFactura=idencFactura " _
        & " WHERE 	fnumero  =  " & NoFactura _
        & " GROUP BY FSERIE,FNUMERO,IDPRODUCTO "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Detalle documentos de cambio de producto
    Public Function xo_get_cambio_Articulo(ByVal NoFactura As String)
        Dim sigla As String
        sigla = " 'CD-'+"
        SQL_QUERY = "" _
        & " SELECT   2 Cabecera ," & sigla & "CONVERT( nvarchar(9), F.fechaEmision, 112 ) + REPLACE (CONVERT( nvarchar(5), F.fNumero, 8  ),':','') +CONVERT(NVARCHAR(5),R.codruta) fnumero,C.idproducto,C.cantidad,'ST' UM " _
        & " FROM [CFACTURA_DETALLE] C " _
        & " inner join CFACTURA F On f.id_encFactura = C.idencFactura " _
        & " left join rruta R ON R.id_ruta = F.idruta " _
        & " WHERE F.id_encFactura  =  " & NoFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT

    End Function

    Public Function xo_get_factura_DetalleArticulo(ByVal noFactura As String, ByVal idproducto As String)
        SQL_QUERY = " " _
        & " SELECT  	 3 Cabecera ,RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura,'ZPRE' claseCONdiciON, sum(d.importeSinIva) importe  " _
        & " FROM 	cfactura F left join cfactura_detalle d ON id_encFactura=idencFactura   " _
        & " WHERE 	fnumero  =    " & noFactura & " AND idproducto =" & idproducto _
        & " group by fserie, fnumero " _
        & " UNION ALL " _
        & " SELECT  	 3 Cabecera,RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura,'ZIVA' claseCONdiciON," _
        & " case when D.importeDestoPP <> 0 then sum(d.valorIvaImporte)  ELSE cONvert(decimal(10,2),sum(d.valorIvaImporte  + d.valorIvaDesto)) END AS IMPORTE  " _
        & " FROM 	cfactura left join cfactura_detalle d ON id_encFactura=idencFactura   " _
        & "  WHERE 	fnumero  =    " & noFactura & " AND idproducto = " & idproducto _
        & " group by fserie, fnumero ,D.importeDestoPP" _
        & "  UNION ALL " _
        & "  SELECT  	 distinct 3 Cabecera ,RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura, " _
        & "  case when D.importeDestoPP <> 0 then 'ZDPP' ELSE 'ZDLI'  END AS claseCONdiciON,  " _
        & "  case when D.importeDestoPP <> 0 then D.importeDestoPP + D.valorIvaDesto  ELSE cONvert(decimal(10,2),d.importeDesto)  END AS IMPORTE  " _
        & "  FROM 	cfactura F left join cfactura_detalle d ON id_encFactura=idencFactura   " _
        & "  WHERE 	fnumero  =    " & noFactura & " AND idproducto = " & idproducto & " AND idRubro = 'L' " _
        & "  UNION ALL " _
        & "  SELECT  	 distinct 3 Cabecera ,RTRIM(fserie)+CONVERT(NVARCHAR(50),fnumero) NumeroFactura,  " _
        & "  'ZPCT'  AS claseCONdiciON,   ABS(D.porcentajeDestoPP)  AS IMPORTE " _
        & "  FROM 	cfactura F left join cfactura_detalle d ON id_encFactura = idencFactura   " _
        & " WHERE 	fnumero  =    " & noFactura & " AND idproducto = " & idproducto & " AND idRubro = 'L' AND D.porcentajedestopp  <> 0 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_Recibo()
        '    SQL_QUERY = "" _
        '& " SELECT  	CASE WHEN CRECIBO.estado = 2 then 9 else 4 end as Cabecera, 'SE' tipoDocumento,idCliente, REPLACE ( CONVERT( nvarchar(15), fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, RTRIM(rserie)+CONVERT(NVARCHAR(50),rnumero) numeroRecibo,RTRIM(d.serie)+CONVERT(NVARCHAR(50),d.numero) documentoAfecta,4000 centroL,11210101 cuentaC,d.importe , CASE WHEN crecibo.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado" _
        '& " FROM 	crecibo LEFT JOIN crecibo_detalle D " _
        '& " ON		id_encrecibo = idencrecibo " _
        '& " WHERE    crecibo.estado = 1  AND D.importe <> 0 "

        SQL_QUERY = "" _
        & " SELECT  	CASE WHEN CRECIBO.estado = 2 then 9 else 4 end as Cabecera, 'SE' tipoDocumento,idCliente, REPLACE ( CONVERT( nvarchar(15), fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, RTRIM(rserie)+CONVERT(NVARCHAR(50),rnumero) numeroRecibo,RTRIM(d.serie)+CONVERT(NVARCHAR(50),d.numero) documentoAfecta, " & id_glo_sociedad & " centroL,11210101 cuentaC,D.importe , D.importe, CASE WHEN crecibo.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, crecibo.ESTADO estado, crecibo.rnumero rnumero " _
        & " FROM 	    crecibo LEFT JOIN crecibo_detalle D " _
        & " ON		    id_encrecibo = idencrecibo " _
        & " WHERE       crecibo.estado = 1  AND D.importe <> 0 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT

        'SQL_QUERY = "" _
        '& " SELECT Cabecera, tipoDocumento, idCliente, fechaEmision, numeroRecibo, CASE WHEN dtipo = 'ND' then 'ND&' + documentoAfecta else documentoAfecta end as documentoAfecta, centroL, cuentaC,importe,qa.des_estado des_estado, estado" _
        '       & " FROM  " _
        '      & " ( " _
        '     & " 	SELECT  	CASE WHEN R.estado = 2 then 9 else 4 end as Cabecera, 'SE' tipoDocumento,R.idCliente, REPLACE ( CONVERT( nvarchar(15), R.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON, RTRIM(rserie)+CONVERT(NVARCHAR(50),rnumero) numeroRecibo,RTRIM(d.serie)+CONVERT(NVARCHAR(50),d.numero) documentoAfecta,4000 centroL,11210101 cuentaC,d.importe , CASE WHEN R.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado,rcxc.dtipo, r.ESTADO estado " _
        '    & " 	FROM 		crecibo r LEFT JOIN crecibo_detalle D  " _
        '& " 	ON			id_encrecibo = idencrecibo  " _
        '       & " 	LEFT JOIN 	RCXC on id_cxc = idencCXC  " _
        '      & " 	WHERE    	r.estado = 1  AND D.importe <> 0  " _
        '     & " )QA "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_recibo_pagos(ByVal NoRecibo As String)
        SQL_QUERY = "" _
        & " SELECT 	5 Cabecera,RTRIM(rserie)+CONVERT(NVARCHAR(50),rnumero) numeroRecibo, C.idCliente,P.idViaPago,P.importe,  " _
        & " 		CASE when P.documento = '0' then '' else p.documento end as noCheque " _
        & " FROM 	crecibo C   " _
        & " 	left join crecibo_detalle D  " _
        & " 		ON	C.id_encrecibo = D.idencrecibo   " _
        & " 		left join crecibo_pagos P  " _
        & " 			ON 	D.idEncRecibo = P.idEncRecibo  " _
        & " WHERE rnumero = " & NoRecibo & " AND C.estado = 1  AND P.importe <> 0  and P.idViaPago <> 'CR'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaCreditoDP_Encabezado()
        SQL_QUERY = "" _
       & " SELECT  		CASE WHEN NC.estado = 2 then 9 else 1 end as Cabecera, ncnumero,'DP' TipoDocumento, nc.idCliente, REPLACE ( CONVERT( nvarchar(15), nc.fechaEmisiON, 111 ) , '/' , '' ) fechaEmisiON,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc,RTRIM(cxc.serie)+CONVERT(NVARCHAR(50),cxc.numero) documentoAfecta, " + id_glo_centro & "  centroL, NC.estado ,NC.importe,CASE WHEN NC.ESTADO = 1 THEN 'EMITIDO' ELSE 'ANULADO' END AS des_estado, NC.serie, NC.numeroautorizacion, NC.preimpreso " _
       & " FROM 		cnotacredito NC " _
       & " LEFT JOIN 	CRECIBO R	ON NC.idEncRecibo = id_encRecibo " _
       & " LEFT JOIN	RCXC CXC	ON R.idEncCxc = id_cxc " _
       & " WHERE  		NC.ttipo = 5 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaCreditoDP_Articulo(ByVal nONotaCredito As String)
        SQL_QUERY = "" _
       & " SELECT  		2 Cabecera,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONNc, d.idProducto,0 cantidad,'ST' UM " _
       & " FROM 		cnotacredito NC " _
       & " LEFT JOIN 	cnotacredito_detalle D ON id_encnc = idEncNc " _
       & " WHERE  		NC.ttipo = 5 " _
       & " AND 		ncNumero =  " & nONotaCredito & " AND idproducto is not null "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaCreditoDP_DetalleArticulo(ByVal nONotaCredito As String, ByVal idproducto As String)
        SQL_QUERY = "" _
        & "  SELECT  	3 Cabecera ,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) NumerONc,'ZSKT' claseCONdiciON,  cONvert(decimal(10,2), sum(d.importeDesto )/(1+avg(d.iva))) importe" _
        & "  FROM 	cnotacredito left join cnotacredito_detalle d ON id_encnc=idencnc" _
        & "  WHERE 	ncnumero  =   " & nONotaCredito & " AND idproducto =  " & idproducto _
        & "  group by  ncserie,ncnumero,idproducto     " _
        & "  UNION ALL    " _
        & "  SELECT  3 Cabecera, RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONc,'ZIVA' claseCONdiciON, " _
        & "  cONvert(decimal(10,2),sum(d.importeDesto) -sum(d.importeDesto )/(1+avg(d.iva)))importe" _
        & "  FROM 	cnotacredito left join cnotacredito_detalle d ON id_encnc=idencnc " _
        & "  WHERE 	ncnumero  =   " & nONotaCredito & " AND idproducto =  " & idproducto _
        & "  group by  ncserie,ncnumero,idproducto "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_NotaCreditoDE_DetalleArticulo(ByVal nONotaCredito As String, ByVal idproducto As String)
        SQL_QUERY = "" _
       & " SELECT  	3 Cabecera ,RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONc,'ZPRE' claseCONdiciON,  sum(d.importeSinIva)  importe  " _
       & " FROM 	cnotacredito left join cnotacredito_detalle d ON id_encnc=idencnc " _
       & " WHERE 	ncnumero  =   " & nONotaCredito & " AND idproducto =  " & idproducto _
       & " group by  ncserie,ncnumero,idproducto " _
       & " UNION ALL   " _
       & " SELECT  	3 Cabecera, RTRIM(ncserie)+CONVERT(NVARCHAR(50),ncnumero) numerONc,'ZIVA' claseCONdiciON, sum(round(d.importeSinIva * d.iva,2))   importe  " _
       & " FROM 	cnotacredito left join cnotacredito_detalle d ON id_encnc=idencnc " _
       & " WHERE 	ncnumero  =   " & nONotaCredito & " AND idproducto =  " & idproducto _
       & " group by  ncserie,ncnumero,idproducto "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
    'Cambios de producto Inventario Encabezado
    Public Function xo_get_Encabezado_Cambio()
        Dim sigla As String
        sigla = "CD"
        SQL_QUERY = "" _
        & " SELECT   DISTINCT 1 Cabecera ,'CD'tipoDocumento, codruta,REPLACE ( CONVERT( nvarchar(15), fechaEmision, 111 ) , '/' , '' ) fechaEmisiON," & sigla & "CONVERT( nvarchar(9), fechaEmision, 112 ) + REPLACE (CONVERT( nvarchar(5), fechaEmision, 8  ),':','') +CONVERT(NVARCHAR(5),codruta) fnumero," & id_glo_usuario.ToString & " as idvendedor,'IL01'CodigoPago,  " & id_glo_centro & " centroL " _
        & " FROM [rruta]  "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function xo_get_DevoluciON_Encabezado(ByVal ttipo As String)
        Dim sigla As String
        If ttipo = 0 Then
            sigla = " 'CE-'+"
        Else
            sigla = " 'PT-'+"
        End If
        SQL_QUERY = "" _
        & " SELECT   DISTINCT 1 Cabecera ,'DI'tipoDocumento, codruta,REPLACE ( CONVERT( nvarchar(15), fechaOperaciON, 111 ) , '/' , '' ) fechaEmisiON," & sigla & "CONVERT( nvarchar(9), fechaOperaciON, 112 ) + REPLACE (CONVERT( nvarchar(5), fechaOperaciON, 8  ),':','') +CONVERT(NVARCHAR(5),codruta) fnumero," & id_glo_usuario.ToString & " as idvendedor,'IL01'CodigoPago,  " & id_glo_centro & " centroL " _
        & " FROM [CBODEGA_LIQUIDACION] C " _
        & " left join rruta ON id_ruta = idruta " _
        & " WHERE unidadesFisico > 0 AND C.ttipo =  " & ttipo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Devolucion Inventario Detalle
    Public Function xo_get_DevoluciON_Detalle(ByVal ttipo As String)
        Dim sigla As String
        If ttipo = 0 Then
            sigla = " 'CE-'+"
        Else
            sigla = " 'PT-'+"
        End If

        SQL_QUERY = "" _
        & " SELECT   2 Cabecera ," & sigla & "CONVERT( nvarchar(9), fechaOperaciON, 112 ) + REPLACE (CONVERT( nvarchar(5), fechaOperaciON, 8  ),':','') + CONVERT(NVARCHAR(5),codruta) fnumero,idproducto,unidadesFisico,'ST' UM " _
        & " FROM [CBODEGA_LIQUIDACION] C " _
        & " left join rruta ON id_ruta = idruta " _
        & " WHERE unidadesFisico > 0 AND C.ttipo =  " & ttipo

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR LIQUIDACION "
    Public Function agregarIntegraciON()
        SQL_QUERY = " " _
        & "SELECT   [ruta],[venta],[envaseRecibido],[creditoConcedido],[descuentoConcedido],[creditoCobrado],[devolucionProducto],[totalLiquidar],[diferenciaCorte],[moneda],[noBoleta],[valorBoleta],[importeDiferencia],[fechaEmision],zcorrelativo " _
        & "FROM    CLIQUIDACION"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR DIFERENCIAS "
    Public Function agregarDiferencia()
        SQL_QUERY = " select * from [CDIFERENCIA_LIQUIDACION]"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR MOVIMIENTO "
    Public Function agregarMovimiento()
        Dim inventario As New InventarioBL
        Return inventario.consultarMovimientoLiquidacion
    End Function
#End Region

#Region " EXPORTAR DETALLE DESPACHO "
    Public Function agregarDetalleDespacho()
        SQL_QUERY = "   SELECT  	 " & id_glo_codRuta & " Ruta ,* ,CASE WHEN despachado  = 'True' THEN 1 ELSE 0 END AS despachado2" _
         & " FROM 	    DESPACHO "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD DE DESPACHO "
    Public Function agregarEfectividadDespacho()
        SQL_QUERY = "   " _
        & "  SELECT DISTINCT  " & id_glo_codRuta & " Ruta ,  " & id_glo_usuario & " AS USUARIO, " _
        & "  SUM(dProgramado)dProgramado,  " _
        & "  SUM(volProgramado)volProgramado,  " _
        & "  SUM(dEntregado)dEntregado,  " _
        & "  SUM(volEntregado)volEntregado,  " _
        & "  CASE WHEN SUM(dProgramado) = 0 THEN 0 ELSE  CONVERT(DECIMAL(18,2), SUM(dEntregado) *100.00 /SUM(dProgramado)) END AS EfectividadEntrega,  " _
        & "  CASE WHEN SUM(volEntregado) = 0 THEN 0 ELSE CONVERT(DECIMAL(18,2), SUM(volEntregado) *100.00 /SUM(volProgramado))  END AS EfectividadVolumen,  " _
        & "  CASE WHEN MAX(fechaConfirma) IS NULL then GETDATE() ELSE  MAX(fechaConfirma) END AS fechaConfirma  " _
        & "  FROM  " _
        & "  (  " _
        & "  SELECT 0 dProgramado,0 volProgramado,count(despachado) dEntregado, SUM(convert(decimal(18,3), LITROS))volEntregado, max(fechaConfirma)fechaConfirma  " _
        & "  FROM DESPACHO   " _
        & "  WHERE DESPACHADO  = 'True'  " _
        & "  OR razonNoDespacho < 20 " _
        & "  UNION ALL  " _
        & "  SELECT count(*) dProgramado,   SUM(convert(decimal(18,3), LITROS)) volProgramado, 0 dEntregado,0 volEntregado , max(fechaConfirma)fechaConfirma " _
        & "  FROM DESPACHO   " _
        & "  )A  " _
        & " HAVING SUM (dEntregado) > 0 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Public Function agregarEfectividadDespacho()
    '    SQL_QUERY = "   " _
    '        & " SELECT [RUTA],FECHA, " _
    '        & " 	SUM([DPROGRAMADO]) DPROGRAMADO, " _
    '        & " 	SUM([VOLPROGRAMADO])VOLPROGRAMADO, " _
    '        & " 	SUM([DENTREGADO])DENTREGADO, " _
    '        & " 	SUM([VOLENTREGADO])VOLENTREGADO, " _
    '        & " 	CASE WHEN SUM(dProgramado) = 0 THEN 0 ELSE  CONVERT(DECIMAL(18,2), SUM(dEntregado) *100.00 /SUM(dProgramado)) END AS EfectividadEntrega, " _
    '        & " 	CASE WHEN SUM(volEntregado) = 0 THEN 0 ELSE CONVERT(DECIMAL(18,2), SUM(volEntregado) *100.00 /SUM(volProgramado))  END AS EfectividadVolumen, " _
    '        & " 	USUARIO, " _
    '        & " 	MAX(HORA) FECHACONFIRMA " _
    '        & " FROM DESPACHOS_X " _
    '        & " GROUP BY RUTA,FECHA,USUARIO "
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD DE COBRO "
    Public Function agregarEfectividadCobro()
        SQL_QUERY = "   " _
         & " SELECT DISTINCT  " & id_glo_codRuta & " Ruta ,  " & id_glo_usuario & " AS USUARIO " _
        & " ,SUM(cProgramado)cProgramado " _
        & " ,SUM(qtzProgramado)qtzProgramado " _
        & " ,SUM(cRealizado)cRealizado " _
        & " ,SUM(qtzRealizado)qtzRealizado " _
        & " ,CASE WHEN SUM(cProgramado) = 0 THEN 0 ELSE  CONVERT(DECIMAL(18,2), SUM(cRealizado) *100.00 /SUM(cProgramado))  END AS  EfectividadCobro " _
        & " ,CASE WHEN SUM(qtzProgramado) = 0 THEN 0 ELSE  CONVERT(DECIMAL(18,2), SUM(qtzRealizado) *100.00 /SUM(qtzProgramado))  END AS  EfectividadRecuperacion " _
        & " ,getdate() fechaOpera " _
        & " FROM " _
        & " 	( " _
        & " 	SELECT 0 cProgramado,0 qtzProgramado,count(pagado) cRealizado, SUM( QA.pagos)qtzRealizado " _
        & " 	FROM RCXC left join  " _
        & " 	( " _
        & " 		SELECT idEncCxc,sum(p.importe) pagos FROM  " _
        & " 		CRECIBO LEFT JOIN CRECIBO_PAGOS p " _
        & " 		ON id_Encrecibo = idEncRecibo " _
        & " 		GROUP BY  idEncCxc " _
        & " 	)QA " _
        & " 	ON ID_CXC = QA.IDENCCXC " _
        & " 	WHERE PAGADO   = 'True'  AND compromisoPago = 'True'  " _
        & " 	UNION ALL  " _
        & " 	SELECT count(compromisoPago) dProgramado,   SUM(importe) volProgramado, 0 dEntregado,0 volEntregado  " _
        & " 	FROM RCXC  " _
        & " 	WHERE  compromisoPago = 'True' " _
        & " 	)A " _
        & " HAVING SUM (cRealizado) > 0 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
#End Region

#Region " EXPORTAR CXC DESMARCA COMPROMISO DE PAGO "
    Public Function agregarCopromisoPago()
        Dim recibo As New Recibo
        Return recibo.getDocumentosEncabezadoCXC()
    End Function
#End Region
End Class
