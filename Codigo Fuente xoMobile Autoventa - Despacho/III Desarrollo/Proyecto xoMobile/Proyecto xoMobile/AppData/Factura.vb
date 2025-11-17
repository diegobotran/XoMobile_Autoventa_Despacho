Imports System.Data
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs
Public Class Factura

    Dim SQL_QUERY As String
    Dim SQL_QUERY2 As String
    Dim SQL_RESULT As Integer
    Dim SQL_RESULT2 As Integer
    Dim SQL_DT As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient


    '---ok
    Public Function crearEncabezado(ByVal OrdenVenta As documentoCO, ByVal Cliente As ClienteCO) As String
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 1: insertar encabezado
        '--- xomobile 2.0 se agrego OrdenVenta.importeDesto, OrdenVenta.importeDestoPP
        SQL_QUERY = "INSERT INTO cfactura " _
        + "(fserie, fnumero, idRuta, fechaEmision, idCliente, nit,importe, moneda, tTipo, idusuario, porcentajeIva, estado, nImpresiones, condicion, idEncRecibo,id_pedido, importeDesto,importeDestoPP) " _
        + " SELECT serie, actual," + id_glo_ruta.ToString() + ",GETDATE(), " + id_glo_cliente.ToString() + ",'" + Cliente.nit + "'," + OrdenVenta.importe + ",'" + co_glo_moneda + "','" _
        + OrdenVenta.ttipo + "'," + id_glo_usuario.ToString() + "," + co_glo_porcentajeIVA + "," + OrdenVenta.estado + ",0 ,'" + OrdenVenta.condicion + "','" & OrdenVenta.idReciboRelacionado & "','" & OrdenVenta.idPedido & "','" & OrdenVenta.importeDesto & "','" & OrdenVenta.importeDestoPP & "'" _
        + " FROM rccorrelativo 	" _
        + " WHERE dtipo = 'F' "

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 2: Obtener el id del encabezado
            SQL_QUERY = "SELECT @@IDENTITY as nn"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 3: Incrementar el correlativo
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'F'"
            objCe.SetExecute(SQL_QUERY)

            '--- Devolver el id del encabezado
            'tx.Commit()
            Return SQL_DT.Rows(0).Item(0).ToString
        Catch
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en las facturas")
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizar_FEL(ByVal idFactura As String, ByVal serie As String, ByVal autorizacion As String, ByVal preimpreso As String, ByVal direccionfel As String, ByVal nombre_fel As String) As Integer
        '+ " ,[estado]            ='" + Factura.estado + "'" _

        Try
            Dim texto As String = ""
            Dim texto1 As String = ""
            texto = Replace(nombre_fel, "'", "")
            nombre_fel = Replace(texto, """", "")
        Catch ex As Exception
            nombre_fel = Replace(nombre_fel, "'", "")
        End Try


        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY2 = "SELECT 'XX', fserie, fnumero FROM cfactura WHERE condicion <> 'IL01' and id_encFactura = " & idFactura
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY2, Conexion)

            If (SQL_DT.Rows(0).Item(0).ToString() = "XX") Then
                SQL_QUERY = " UPDATE RCXC SET " _
                    + " seriefel ='" & serie & "'" _
                    + " ,numeroautorizacion='" & preimpreso & "'" _
                    + " WHERE serie   = '" & SQL_DT.Rows(0).Item(1).ToString() & "'" _
                    + " AND numero   = '" & SQL_DT.Rows(0).Item(2).ToString() & "'"
                SQL_RESULT = objCe.SetExecute(SQL_QUERY)
            End If
            Conexion.Close()
        Catch ex As Exception
            Conexion.Close()
        End Try

        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY2 = "SELECT numeroacceso FROM cfactura WHERE id_encFactura = " & idFactura
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY2, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString().Length > 0) Then
                SQL_QUERY = " UPDATE RCONTINGENCIA SET " _
                    + " serie ='" & serie & "'" _
                    + " ,preimpreso ='" & preimpreso & "'" _
                    + " ,fecha_sat = getdate()" _
                    + " WHERE numeroAcceso   = '" & SQL_DT.Rows(0).Item(0).ToString() & "'"
                SQL_RESULT = objCe.SetExecute(SQL_QUERY)
            End If
            Conexion.Close()
        Catch ex As Exception
            Conexion.Close()
        End Try





        SQL_QUERY = " UPDATE CFACTURA SET " _
       + " serie ='" + serie + "'" _
       + " ,numeroautorizacion='" + autorizacion + "'" _
       + " ,preimpreso='" + preimpreso + "'" _
       + " ,nombre_fel='" & nombre_fel & "'" _
       + " ,direccionfel='" + limpiarCadenaNombreFichero(direccionfel, "") + "'" _
       + " WHERE id_encFactura   = " + idFactura
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT

    End Function

    Public Function revertirCXCFact(ByVal serie As String, ByVal numero As Integer)
        SQL_QUERY = _
        "   DELETE FROM  RCXC " _
        & " WHERE  serie = '" & serie & "' AND numero=" & numero
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
        "   DELETE FROM  RCXC_DETALLE " _
        & " WHERE  idCxc = '" & serie & numero & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        Return SQL_RESULT

    End Function

    Function limpiarCadenaNombreFichero(ByVal cadenaTexto As String, ByVal sustituirPor As String) As String
        Dim tamanoCadena, cadenaResultado, caracteresValidos As String
        cadenaResultado = ""
        Dim caracterActual As String
        Dim i As Integer = 0
        tamanoCadena = Len(cadenaTexto)
        If tamanoCadena > 0 Then
            caracteresValidos = _
                " 0123456789abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ-_."
            For i = 1 To tamanoCadena
                caracterActual = Mid(cadenaTexto, i, 1)
                If InStr(caracteresValidos, caracterActual) Then
                    cadenaResultado = cadenaResultado & caracterActual
                Else
                    cadenaResultado = cadenaResultado & sustituirPor
                End If
            Next
        End If
    End Function


    Public Function actualizar_FELNC(ByVal idNc As String, ByVal serie As String, ByVal autorizacion As String, ByVal preimpreso As String, ByVal direccionfel As String, ByVal nombre_fel As String) As Integer
        '+ " ,[estado]            ='" + Factura.estado + "'" _


        Try
            Dim texto As String = ""
            Dim texto1 As String = ""
            texto = Replace(nombre_fel, "'", "")
            nombre_fel = Replace(texto, """", "")
        Catch ex As Exception
            nombre_fel = Replace(nombre_fel, "'", "")
        End Try


        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY2 = "SELECT numeroacceso FROM cnotacredito WHERE id_EncNc = " & idNc
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY2, Conexion)

            If (SQL_DT.Rows(0).Item(0).ToString().Length > 0) Then
                SQL_QUERY = " UPDATE RCONTINGENCIA SET " _
                    + " serie ='" & serie & "'" _
                    + " ,preimpreso ='" & preimpreso & "'" _
                    + " ,fecha_sat = getdate()" _
                    + " WHERE numeroAcceso   = '" & SQL_DT.Rows(0).Item(0).ToString() & "'"
                SQL_RESULT = objCe.SetExecute(SQL_QUERY)
            End If
            Conexion.Close()
        Catch ex As Exception
            Conexion.Close()
        End Try

        SQL_QUERY = " UPDATE CNOTACREDITO SET " _
       + " serie ='" + serie + "'" _
       + " ,numeroautorizacion='" + autorizacion + "'" _
       + " ,preimpreso='" + preimpreso + "'" _
       + " ,nombre_fel='" & nombre_fel & "'" _
       + " ,direccionfel='" + limpiarCadenaNombreFichero(direccionfel, "") + "'" _
       + " WHERE id_encNc   = " + idNc
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function


    '---ok
    Public Function crearEncabezadoCambio(ByVal OrdenVenta As documentoCO, ByVal Cliente As ClienteCO) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 1: insertar encabezado
        '--- xomobile 2.0 se agrego OrdenVenta.importeDesto, OrdenVenta.importeDestoPP
        SQL_QUERY = "INSERT INTO cfactura " _
        + "(fserie, fnumero, idRuta, fechaEmision, idCliente, nit,importe, moneda, tTipo, idusuario, porcentajeIva, estado, nImpresiones, condicion, idEncRecibo,id_pedido, importeDesto,importeDestoPP,tMotivo) " _
        + " SELECT serie, actual," + id_glo_ruta.ToString() + ",GETDATE(), " + id_glo_cliente.ToString() + ",'" + Cliente.nit + "'," + OrdenVenta.importe + ",'" + co_glo_moneda + "','" _
        + OrdenVenta.ttipo + "'," + id_glo_usuario.ToString() + "," + co_glo_porcentajeIVA + "," + OrdenVenta.estado + ",0 ,'" + OrdenVenta.condicion + "','" & OrdenVenta.idReciboRelacionado & "','" & OrdenVenta.idPedido & "','" & OrdenVenta.importeDesto & "','" & OrdenVenta.importeDestoPP & "','" & OrdenVenta.motivo & "'" _
        + " FROM rccorrelativo 	" _
        + " WHERE dtipo = 'CD' "

        Conexion.Open()
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT @@IDENTITY as nn"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'CD'"
        objCe.SetExecute(SQL_QUERY)

        '--- Cerrar la conexion
        Conexion.Close()

        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString
    End Function

    Public Function agregarItem(ByVal item As ItemCO) As Integer
        'Se agrega para realizar las pruebas de validacion 
        'item.importe = 10
        SQL_QUERY = "INSERT INTO cfactura_Detalle " _
        + "(idEncFactura, item, idproducto, cantidad, um, precio, importeSinIva,importe, " _
        + " iva, porcentajeDesto, importeDesto,tipoVenta, " _
        + " porcentajeDestoPP, idRubro,importeDestoPP,trqt,litm, estado,valorIvaDesto, valorIvaImporte ) " _
        + "values(" + item.idEncabezado + "," + item.NoItem + "," + item.idProducto + "," + item.cantidad + ",'" + item.um + "'," + item.precio + "," + item.importeSinIva + "," + item.importe + "," _
        + item.iva + "," + item.porcentajeDesto + "," + item.importeDesto.ToString + ",'" + item.tipoVenta + "'," _
        + item.porcentajeDestoPP + ",'" + item.idRubro + "'," + item.importeDestoPP.ToString() + ",'" + item.trqt + "'," + item.litm + "," + item.estado + ",'" + item.valorIvaDesto + "','" + item.valorIvaImporte + "')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function


    Public Function eliminar() As Integer

        SQL_QUERY = _
        "DELETE FROM CFACTURA_DETALLE WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       "DELETE FROM CFACTURA WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Query 3: Decrementar el correlativo
        If SQL_RESULT <> 0 Then
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual -1 WHERE  dtipo = 'F'"
            objCe.SetExecute(SQL_QUERY)
        End If

        Return SQL_RESULT
    End Function

    'Public Function eliminarDetalle() As Integer

    '    SQL_QUERY = _
    '   "DELETE FROM CFACTURA_DETALLE WHERE estado = 0 "
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    '    Return SQL_RESULT

    'End Function

    'Public Function getFacturaItem(ByVal idFactura As String) As DataTable
    '    SQL_QUERY = "SELECT  * FROM cfactura_detalle WHERE idEncFactura = 	" + idFactura
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    Public Function getConsulta() As DataTable
        SQL_QUERY = " select D.id_encFactura,  C.id_cliente Codigo , C.negocio Cliente, D.tTipo tTipo , D.FSerie Serie , D.serie Seriefel, D.numeroautorizacion, D.preimpreso, FNumero Numero,convert(nvarchar(10),fechaEmision,103) as Femi, convert(nvarchar(10),fechaEmision,108) as Hemi,Importe Importe, estado, nImpresiones,idEncRecibo, numeroacceso, " _
        + " CASE when importeDesto = 0 then importeDestoPP else importeDesto END as importeDesto, " _
        + " CASE when importeDestoPP <> 0 then 'Futuro' else 'Aplicado' END as  tipoDescuento " _
        + " from [CFACTURA] D " _
        + " left join [RCLIENTE] C " _
        + " on C.id_cliente=D.idcliente " _
        + " where ttipo   NOT IN  ('PED','CD') " _
        & " order by id_encFactura desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getConsultaCambios() As DataTable
        SQL_QUERY = " select D.id_encFactura,  C.id_cliente Codigo , C.negocio Cliente , D.FSerie Serie, D.serie Seriefel, D.numeroautorizacion, D.preimpreso, FNumero Numero,convert(nvarchar(10),fechaEmision,103) as Femi, convert(nvarchar(10),fechaEmision,108) as Hemi,Importe Importe, estado, nImpresiones,idEncRecibo, " _
        + " CASE when importeDesto = 0 then importeDestoPP else importeDesto END as importeDesto, " _
        + " CASE when importeDestoPP <> 0 then 'Futuro' else 'Aplicado' END as  tipoDescuento " _
        + " from [CFACTURA] D " _
        + " left join [RCLIENTE] C " _
        + " on C.id_cliente=D.idcliente " _
        + " where ttipo   IN  ('CD') " _
        & " order by id_encFactura desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Public Function getSumDescuento(ByVal idEncabezado) As String
    '    SQL_QUERY = "SELECT SUM(importeDesto)importeDesto from cfactura_detalle where idencFactura =" + idEncabezado
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT.Rows(0).Item("importeDesto")
    'End Function

    'Public Function setDescuentoEncabezado(ByRef factura As documentoCO) As Integer
    '    SQL_QUERY = "UPDATE cfactura SET importeDesto = " + factura.importeDesto + ", importeDestoPP = " + factura.importeDestoPP + ", importeDestoEnv = " + factura.importeDestoEnv + ", condicion = '" & factura.condicion & "'  where id_encFactura = " + factura.idEncabezado
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    '    Return SQL_RESULT
    'End Function

    'Public Function liberarDPP(ByVal idFactura As String) As Integer
    '    SQL_QUERY = " UPDATE cfactura_detalle " _
    ' + " SET importeDestoPP      = 0" _
    ' + " ,   porcentajeDestoPP   = 0" _
    ' + " WHERE idencFactura = " + idFactura
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    'End Function

    'Public Function liberarNormal(ByVal idFactura As String) As Integer
    '    SQL_QUERY = "UPDATE cfactura_detalle SET importeDesto = 0, porcentajeDesto = 0  WHERE idEncFactura = 	" + idFactura
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    'End Function

    Public Function getImporteLiquido(ByVal idFactura As String) As String
        Dim importe As Decimal
        SQL_QUERY = "SELECT SUM(importe)importe from cfactura_detalle " _
           + " WHERE idencFactura = " + idFactura + " and idRubro = 'L' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        If SQL_DT.Rows.Count > 0 Then
            importe = SQL_DT.Rows(0).Item("importe")
        Else
            importe = 0
        End If
        Return importe
    End Function

    Public Function setNoImpresiones(ByVal idFactura As String, ByVal numero As String) As Integer
        SQL_QUERY = " UPDATE cfactura " _
        + " SET nImpresiones = nImpresiones + " + numero _
        + " WHERE id_encFactura = " + idFactura
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function getFactura(ByVal idFactura As String) As DataTable
        SQL_QUERY = " SELECT  F.*, C.noResolucion, C.fechaResolucion,C.inicial,C.final FROM cfactura F  " _
        + " LEFT JOIN  [RCCORRELATIVO]C on F.idruta = C.idruta " _
        + " WHERE dTipo = 'F' AND id_encFactura = " + idFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getFacturaCambio(ByVal idFactura As String) As DataTable
        SQL_QUERY = " SELECT  F.*, C.noResolucion, C.fechaResolucion,C.inicial,C.final FROM cfactura F  " _
        + " LEFT JOIN  [RCCORRELATIVO]C on F.idruta = C.idruta " _
        + " WHERE dTipo = 'CD' AND id_encFactura = " + idFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getFacturaDetalle(ByVal idFactura As String) As DataTable
        SQL_QUERY = " SELECT	NC.* , P.descripcion  " _
        + " FROM 	[CFACTURA_DETALLE] NC " _
        + " LEFT JOIN RPRODUCTO     P  " _
        + " ON NC.litm       =   P.id_producto  " _
        + " WHERE 	idEncFactura  =    " + idFactura _
        + " ORDER BY ITEM "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getFacturaPagoEfectivo(ByVal idFactura As String, ByVal tipoPago As String, ByVal idRecibo As String) As DataTable
        SQL_QUERY = " select pag.idEncRecibo, pag.idViaPago, pag.importe, pag.estado " _
        + " from cfactura fact, cRecibo rec, cRecibo_pagos pag " _
        + " where fact.idEncRecibo = rec.id_encRecibo  " _
        + " and rec.id_encRecibo = pag.idEncRecibo  " _
        + " and fact.id_EncFactura =    " + idFactura _
        + " and rec.id_EncRecibo =    " + idRecibo _
        + " and rec.estado <> 2 and pag.estado <> 2 and fact.estado = 1  " _
        + " and pag.idViaPago = '" & tipoPago & "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNotaDetalle(ByVal idFactura As String) As DataTable
        SQL_QUERY = " SELECT	NC.* , P.descripcion  " _
        + " FROM 	[CNOTACREDITO_DETALLE] NC  " _
        + " LEFT JOIN RPRODUCTO     P  " _
        + " ON NC.idProducto       =   P.id_producto  " _
        + " INNER JOIN [CNOTACREDITO] NT " _
        + " ON NC.idEncnc = NT.id_Encnc " _
        + " WHERE 	NT.idEncRecibo  =    " + idFactura _
        + " ORDER BY ITEM "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function anularDocumentosSin()
        SQL_QUERY2 = "UPDATE cfactura set estado = 2, fechaAnula = getDate() WHERE id_encfactura in ( " _
                     + " SELECT id_encfactura from cfactura " _
                     + " WHERE(estado = 1) " _
                     + " AND id_encfactura NOT in ( " _
                     + " Select a.id_encfactura " _
                     + " from cfactura a, cfactura_detalle b " _
                     + " where(a.id_encfactura = b.idEncfactura) " _
                     + " and  a.estado = 1 " _
                     + " group by a.id_encfactura) )"
        SQL_RESULT2 = objCe.SetExecute(SQL_QUERY2)
        Return SQL_RESULT2
    End Function

    Public Function actualizar(ByVal Factura As documentoCO, Optional ByVal iscontado As Boolean = False, Optional ByVal manipulaDescuentos As Boolean = True) As Integer

        SQL_QUERY = " UPDATE CFACTURA SET " _
               & "  [usuarioAnula]      = '" & Factura.usuarioAnula & "'" _
               & " ,[fechaAnula]        = " & Factura.fechaAnula & "" _
               & " ,[nImpresiones]      = '" & Factura.nImpresiones & "'" _
               & " ,[idEncRecibo]       = '" & Factura.idReciboRelacionado & "'" _
               & " ,[estado]            = '" & Factura.estado & "'" _
               & " ,[fechaEmision]      = " & Factura.fechaEmision & "" _
               & " ,[fechavence]        = " & Factura.fechaVence & "" _
               & " ,[porcentajeIva]     = '" & Factura.porcentajeIva & "'" _
               & " ,[fserie]            = '" & Factura.serie & "'" _
               & " ,[fnumero]           = '" & Factura.numero & "'" _
               & " ,[ttipo]             = '" & Factura.ttipo & "'" _
               & " ,[idusuario]         = '" & Factura.idusuario & "'" _
               & " ,[importeDestoEnv]   = '" & Factura.importeDestoEnv & "'" _
               & " WHERE id_encFactura  =  " & Factura.idEncabezado
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)



        '--- Procesos cuando no son despacho
        If tipoRuta <> "16" And manipulaDescuentos Then

            SQL_QUERY = " UPDATE CFACTURA SET " _
             & "  [condicion]               = '" & id_glo_condicion & "'" _
             & " ,[importeDestoPP]          =  " & Factura.importeDestoPP & "" _
             & " ,[importeDesto]            = '" & Factura.importeDesto & "'" _
             & " WHERE id_encFactura        =  " & Factura.idEncabezado
            SQL_RESULT = objCe.SetExecute(SQL_QUERY)


            If iscontado Then
                SQL_QUERY = " UPDATE CFACTURA_DETALLE SET " _
                   & "  [importeDestoPP]            = '" + Factura.importeDestoPP & "'" _
                   & " ,[porcentajeDestoPP]         = '" + Factura.importeDestoPP & "'" _
                   & "  WHERE idencFactura          = " + Factura.idEncabezado
            Else
                SQL_QUERY = " UPDATE CFACTURA_DETALLE SET " _
                  & " [importeDesto]              = '" + Factura.importeDesto & "'" _
                  & " ,[porcentajeDesto]          = '" + Factura.importeDesto & "'" _
                  & " WHERE idencFactura          = " + Factura.idEncabezado
            End If
            SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        End If
        Return True
    End Function




    Public Function actualizarDetalle(ByVal idFacturaOriginal As String, ByVal idFacturaCopia As String) As Integer

        SQL_QUERY = " UPDATE CFACTURA_DETALLE SET " _
               & "  [idEncFactura]      = '" & idFacturaCopia & "'" _
               & "  WHERE idencFactura  =  " & idFacturaOriginal
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setCopiaFactura(ByVal idFactura As String) As Integer

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        SQL_QUERY = "  " _
               & " insert into cfactura ([idRuta],[fSerie],[fNumero],[fechaEmision],[fechaVence],[idCliente],[nit],[importe],[moneda],[importeDesto],[importeDestoPP],[importeDestoEnv],[usuarioAnula],[fechaAnula],[porcentajeIva],[tTipo],[idusuario],[estado],[idEncRecibo],[condicion],[id_Pedido],[nImpresiones],[noEntrega],[noPedido]) " _
               & " select [idRuta],[fSerie],[fNumero],[fechaEmision],[fechaVence],[idCliente],[nit],[importe],[moneda],[importeDesto],[importeDestoPP],[importeDestoEnv],[usuarioAnula],[fechaAnula],[porcentajeIva],[tTipo],[idusuario],[estado],[idEncRecibo],[condicion],[id_Pedido],[nImpresiones],[noEntrega],[noPedido] " _
               & " from cfactura where id_encfactura = " & idFactura
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

    Public Function incrementCorrelativo(ByVal tipoDocumento As String) As Integer
        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = '" & tipoDocumento & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actulizaContingencia(ByVal documento As String, ByVal contingencia As String, ByVal tipo_receptor As String, ByVal idreceptor As String) As Integer
        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE cfactura SET numeroacceso  = " & contingencia & ", tipo_receptor = " & tipo_receptor & ", idreceptor = '" & idreceptor & "' WHERE  id_EncFactura  = '" & documento & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function obtenerContingencia(ByVal idFactura As String) As Integer

        Conexion = objCe.dbConnect()
        Conexion.Open()
        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT numeroAcceso from rcontingencia where usado <> 'X'"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Cerrar la conexion
        Conexion.Close()

        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString
    End Function

    Public Function actulizaNumeroAcceso(ByVal contingencia As String, ByVal serie As String, ByVal numero As String, ByVal cliente As String) As Integer
        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE rcontingencia SET usado  = 'X', registrado = 'X', referencia='" & serie & "-" & numero & "', fecha = '" & Format(CDate(DateTime.Now), "yyyy-MM-dd").ToString() & "', hora = '" & Format(CDate(DateTime.Now), "Hmmss") & "'," & " codigo_cliente = '" & cliente & "'" & " WHERE  numeroAcceso  = '" & contingencia & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actulizaNumeroAcceso2(ByVal contingencia As String) As Integer
        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE rcontingencia SET usado  = 'X', fecha = '" & Format(CDate(DateTime.Now), "yyyy-MM-dd").ToString() & "', hora = '" & Format(CDate(DateTime.Now), "Hmmss") & "' WHERE  numeroAcceso  = '" & contingencia & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actulizaContingencianc(ByVal documento As String, ByVal contingencia As String, ByVal tipo_receptor As String, ByVal idreceptor As String) As Integer
        '--- Query 3: Incrementar el correlativo
        SQL_QUERY = "UPDATE cNotacredito SET numeroacceso  = " & contingencia & ", tipo_receptor = " & tipo_receptor & ", idreceptor = '" & idreceptor & "' WHERE  id_EncNc  = '" & documento & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function cambiarResponsable(ByVal clienteT As String, ByVal clienteSAP As String) As Integer

        SQL_QUERY = _
        " UPDATE 	CFACTURA  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       " UPDATE 	CRECIBO  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
      " UPDATE 	    CNOTACREDITO  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
        " UPDATE 	CBITACORA  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
        " UPDATE 	CENCUESTA  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       " UPDATE 	MOVIMIENTO_INVENTARIO  SET 		idcliente = " & clienteSAP & "  WHERE idcliente  = " & clienteT
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function
End Class
