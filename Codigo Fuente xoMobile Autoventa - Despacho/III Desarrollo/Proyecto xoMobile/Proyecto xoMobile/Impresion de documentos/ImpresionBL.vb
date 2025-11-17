Imports System
Imports System.IO.Ports
Imports System.IO
Imports System.Data
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs


'---ok
Public Class ImpresionBL
    Public mAncho As Integer = 55 '40
    Dim Cadena_impresion As String
    Dim Puerto As String
    Dim Velocidad As Long
    Dim objCe As New ceClient
    Dim Conexion As SqlCeConnection
    Dim objimpresion As New Impresion
    Dim objDocumento As New DocumentoBL
    Dim objCliente As New ClienteBL
    Dim objUtil As New UtilitarioBL
    Dim times As Integer = 3

    Public Function imprimeFacturaContingencia(ByVal idFactura As String, ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim desct As Decimal = 0            'Variable que permite llevar la sumatoria de descuento
        Dim totd As Decimal = 0             'Variable que permite llevar la sumatoria del total del documento
        Dim Texto As String = ""            'Texto para la impresión 
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtFactura As DataTable
        Dim dtPagos As DataTable
        Dim importeCaja, importeEnvase, importeLiquido As Decimal
        Dim pieFactura As String = ""
        Dim isCredito As Boolean = False
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim headPrinted As Boolean = False

        dtFactura = objDocumento.getFacturaDetalle(idFactura)
        factura = objDocumento.getFactura(idFactura)
        cliente = objCliente.getDetalleDelCliente(factura.idCliente)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        Ruta = objRuta.getActiva()

        For i As Integer = 0 To dtPagos.Rows.Count - 1
            If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                isCredito = True
            End If
        Next

        With objimpresion
            objimpresion.Imprime_Encabezado_doc(Texto)
            Texto = Texto + objimpresion.AlinCent(" DOCUMENTO TRIBUTARIO ELECTRONICO ", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent(" DOCUMENTO EN CONTINGENCIA  ", mAncho) + vbCrLf
            'Texto = Texto + objimpresion.AlinCent("NO.: " + Trim(factura.noResolucion) + " DE FECHA: " + FormatDateTime(factura.fechaResolucion, DateFormat.ShortDate), mAncho) + vbCrLf
            'Texto = Texto + objimpresion.AlinCent("Serie: " + factura.serie + "  Del " + factura.inicial + " al " + factura.final, mAncho) + vbCrLf

            If (isCredito = True) Then
                Texto = Texto + objimpresion.AlinCent("F A C T U R A   C A M B I A R I A - LIBRE  DE  PROTESTO", mAncho) + vbCrLf
            Else
                Texto = Texto + objimpresion.AlinCent(" F  A  C  T  U  R  A  ", mAncho) + vbCrLf
            End If

            Texto = Texto + objimpresion.AlinCent("NUMERO DE ACCESO: " & factura.numeroacceso, mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("obtenga el DTE Certificado en el sitio ", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("www.sat.gob.gt/efactura", mAncho) + vbCrLf
            'Texto = Texto + objimpresion.AlinCent("SERIE: " + factura.serieFEL + " NUMERO: " + factura.preimpreso, mAncho) + vbCrLf
            'Texto = Texto + objimpresion.AlinCent("Numero Autorizacion: " + factura.UUID, mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("", mAncho) + vbCrLf

            Texto = Texto + "LUGAR DE CREACION: " + objimpresion.partir_textoI(LTrim(RTrim(cliente.direccion)), 35) + vbCrLf
            Texto = Texto + "FECHA     : " + Format(CDate(factura.fechaEmision), "dd/MM/yyyy") & vbCrLf
            If Ruta.clienteGenerico = cliente.codigo Then
                If (factura.ttipo = "ZTAE") Then
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                    If factura.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    End If
                    If factura.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(cliente.numeroDi, 50) + vbCrLf
                    End If

                    If factura.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(cliente.numeroDi, 50) + vbCrLf
                    End If

                    Texto = Texto + "NEGOCIO   :____________________________________________" + vbCrLf
                Else
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : CONSUMIDOR  FINAL" + vbCrLf
                    Texto = Texto + "NIT       : C / F " + vbCrLf
                    Texto = Texto + "NEGOCIO   : CONSUMIDOR  FINAL" + vbCrLf
                End If
            Else
                Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                If factura.tipoReceptor = 4 Then
                    Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                End If
                If factura.tipoReceptor = 2 Then
                    Texto = Texto + "CUI       : " + .AlinIzq(cliente.numeroDi, 50) + vbCrLf
                End If

                If factura.tipoReceptor = 3 Then
                    Texto = Texto + "Pasaporte       : " + .AlinIzq(cliente.numeroDi, 50) + vbCrLf
                End If
                Texto = Texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
            End If
            Dim msg = "Desea imprimir la dirección "
            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, "xoMobile")
            If response = MsgBoxResult.Yes Then
                Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
            End If

            Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "COD.        PRODUCTO " + vbCrLf
            Texto = Texto + "C.U.        PRECIO           DESCUENTO        IMPORTE" + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + "@"
            '---Detalle de la factura
            For i As Integer = 0 To dtFactura.Rows.Count - 1

                item = objDocumento.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                item.iva = co_glo_porcentajeIVA

                If item.idRubro = "L" Or (factura.ttipo = "ZTAP" Or factura.ttipo = "ZTAE") Then
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                    Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                    headPrinted = True
                    Texto = Texto + "@"
                End If

                Select Case item.idRubro
                    Case "L"

                        If id_glo_sociedad = 7000 Then
                            importeLiquido = importeLiquido + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "    0/" + Trim(item.cantidad)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency((item.importe / item.cantidad), 2), 15)


                            '--- Omitir registro del descuento si es credito
                            Dim desto As Decimal = 0
                            If factura.condicion = "IL01" Then
                                desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                            End If
                            desct = desct + desto           'Sumatoria del descuento
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal
                        Else
                            importeLiquido = importeLiquido + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)


                            '--- Omitir registro del descuento si es credito
                            Dim desto As Decimal = 0
                            If factura.condicion = "IL01" Then
                                desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                            End If
                            desct = desct + desto           'Sumatoria del descuento
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal
                        End If

                    Case "E"
                        importeEnvase = importeEnvase + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal

                    Case "C"
                        '--- Imprimir el nombre del material si no se ha hecho
                        If Not headPrinted Then printMaterialName(Texto, item)
                        importeCaja = importeCaja + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal
                End Select

                If i = times Then
                    times = times + 3
                    Texto = Texto + "@"
                End If
                'Se agrego corte
                Texto = Texto + "@"
            Next

            '--- Pie de la factura
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(factura.importe, 2), 15) + vbCrLf
            Texto = Texto + "@"
            Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(totd, 2), 15) + vbCrLf
            Texto = Texto + "L I Q U I D O                           " + .AlinDer(FormatCurrency(importeLiquido, 2), 15) + vbCrLf
            Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
            Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "@"
            If factura.importeDesto <> 0 Then
                'Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(factura.importeDesto * -1, 2), 15) + vbCrLf
                Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(desct * -1, 2), 15) + vbCrLf
            End If
            'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importe) + objUtil.isDecimal(factura.importeDesto), 2), 15) + vbCrLf
            Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(totd) + objUtil.isDecimal(desct), 2), 15) + vbCrLf
            Texto = Texto + "F O R M A   P A G O                     " + vbCrLf
            Texto = Texto + "@"
            '--- Formas de pago
            For i As Integer = 0 To dtPagos.Rows.Count - 1
                Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                Texto = Texto + dtPagos.Rows(i).Item("Descripcion") & vbTab & "(+)"
                Texto = Texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                    pieFactura = objimpresion.AlinCent("UD. TIENE " + Trim(objUtil.getDataValue("CPAGO", factura.condicion)) + " DIAS DE CREDITO AUTORIZADOS.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("@ME COMPROMETO A CANCELARLA EL ", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent(Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SI USTED PAGA A MAS TARDAR EL " + FormatDateTime(factura.fechaVence, DateFormat.ShortDate).ToString(), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("TIENE DERECHO A UN DESCUENTO DE " + FormatCurrency(objUtil.isDecimal(factura.importeDestoPP) * -1, 2), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SIEMPRE Y CUANDO  NO DEVUELVA PRODUCTO.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("RECIBI DE CONFORMIDAD LOS PRODUCTOS", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("DETALLADOS EN ESTA FACTURA CAMBIARIA.", mAncho) + vbCrLf
                    isCredito = True
                End If
            Next
            Texto = Texto + "ENVASE  " & vbTab & "(-)" + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importeDestoEnv), 2), 44) + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
            Texto = Texto + objimpresion.AlinCent("Admin: " + factura.serie + "-" + factura.numero, mAncho) + vbCrLf
            Texto = Texto + .AlinCent("Sujeto a pagos trimestrales", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("AGENTE DE RETENCION DEL IVA", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf

            'Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
            'Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf

            Texto = Texto + pieFactura
            Texto = Texto + vbCrLf
            Texto = Texto + vbCrLf


            texto_1 = Texto
            texto_2 = Texto

        End With

        '--- Crear la copia en archivo de texto
        textToFile("FACT_" + factura.serie + factura.numero + ".txt", Texto)

        If preguntaSiImprimir Then
            If Not objimpresion.ConfirmaImpresion("Factura") Then
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                Return False
            End If
        End If

        texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
        texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
        texto_1 = texto_1 + vbCrLf
        texto_1 = texto_1 + vbCrLf


        Dim arreglo As String()
        arreglo = texto_1.ToString.Split("@")
        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
            texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
            texto_2 = texto_2 + vbCrLf
            texto_2 = texto_2 + vbCrLf

            If isCredito Then
                arreglo = texto_2.ToString.Split("@")
                objimpresion.Imprime_Documento_grande(arreglo)
            End If
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 1)
            Return True
        Else
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
            Return False
        End If
    End Function

    Public Function imprimeFactura(ByVal idFactura As String, ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim desct As Decimal = 0            'Variable que permite llevar la sumatoria de descuento
        Dim totd As Decimal = 0             'Variable que permite llevar la sumatoria del total del documento
        Dim Texto As String = ""            'Texto para la impresión 
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtFactura As DataTable
        Dim dtPagos As DataTable
        Dim importeCaja, importeEnvase, importeLiquido As Decimal
        Dim pieFactura As String = ""
        Dim isCredito As Boolean = False
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim headPrinted As Boolean = False

        dtFactura = objDocumento.getFacturaDetalle(idFactura)
        factura = objDocumento.getFactura(idFactura)
        cliente = objCliente.getDetalleDelCliente(factura.idCliente)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        Ruta = objRuta.getActiva()


        For i As Integer = 0 To dtPagos.Rows.Count - 1
            If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                isCredito = True
            End If
        Next


        If (Len(factura.preimpreso) > 0) Then
            With objimpresion
                objimpresion.Imprime_Encabezado_doc(Texto)
                Texto = Texto + objimpresion.AlinCent(" DOCUMENTO TRIBUTARIO ELECTRONICO ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("NO.: " + Trim(factura.noResolucion) + " DE FECHA: " + FormatDateTime(factura.fechaResolucion, DateFormat.ShortDate), mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("Serie: " + factura.serie + "  Del " + factura.inicial + " al " + factura.final, mAncho) + vbCrLf
                If (isCredito = True) Then
                    Texto = Texto + objimpresion.AlinCent("F A C T U R A   C A M B I A R I A - LIBRE  DE  PROTESTO", mAncho) + vbCrLf
                Else
                    Texto = Texto + objimpresion.AlinCent(" F  A  C  T  U  R  A  ", mAncho) + vbCrLf
                End If
                'Texto = Texto + objimpresion.AlinCent(" DOCUMENTO EN CONTINGENCIA  ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("NUMERO DE ACCESO: 846900589", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("obtenga el DTE Certificado en el sitio ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("www.sat.gob.gt/efactura", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("SERIE: " + factura.serieFEL + " NUMERO: " + factura.preimpreso, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Autorizacion: " + factura.UUID, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("", mAncho) + vbCrLf

                Texto = Texto + "LUGAR DE CREACION: " + objimpresion.partir_textoI(LTrim(RTrim(cliente.direccion)), 35) + vbCrLf
                Texto = Texto + "FECHA     : " + Format(CDate(factura.fechaEmision), "dd/MM/yyyy") & vbCrLf
                If Ruta.clienteGenerico = cliente.codigo Then
                    If (factura.ttipo = "ZTAE") Then
                        'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                        Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                        If factura.tipoReceptor = 4 Then
                            Texto = Texto + "NIT       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        Else
                        End If
                        If factura.tipoReceptor = 2 Then
                            Texto = Texto + "CUI       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        End If
                        If factura.tipoReceptor = 3 Then
                            Texto = Texto + "Pasaporte       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        End If
                        Texto = Texto & "NOMBRE    : " & .AlinIzq(factura.nombre_fel, 50) & vbCrLf
                        Texto = Texto + "NEGOCIO   :____________________________________________" + vbCrLf
                    Else
                        'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                        Texto = Texto + "NOMBRE    : CONSUMIDOR  FINAL" + vbCrLf
                        Texto = Texto + "NIT       : C / F " + vbCrLf
                        Texto = Texto + "NEGOCIO   : CONSUMIDOR  FINAL" + vbCrLf
                    End If
                Else
                    Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    If factura.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    Else
                    End If
                    If factura.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    End If
                    If factura.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    End If
                    Texto = Texto & "NOMBRE    : " & .AlinIzq(factura.nombre_fel, 50) & vbCrLf
                    Texto = Texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
                End If
                Dim msg = "Desea imprimir la dirección "
                Dim style = MsgBoxStyle.YesNo
                Dim response = MsgBox(msg, style, "xoMobile")
                If response = MsgBoxResult.Yes Then
                    Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
                End If

                Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Texto = Texto + "COD.        PRODUCTO " + vbCrLf
                Texto = Texto + "C.U.        PRECIO           DESCUENTO        IMPORTE" + vbCrLf
                Texto = Texto + vbCrLf
                Texto = Texto + "@"
                '---Detalle de la factura
                For i As Integer = 0 To dtFactura.Rows.Count - 1

                    item = objDocumento.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    item.iva = co_glo_porcentajeIVA

                    If item.idRubro = "L" Or (factura.ttipo = "ZTAP" Or factura.ttipo = "ZTAE") Then
                        Texto = Texto + "-------------------------------------------------------" + vbCrLf
                        Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                        Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                        headPrinted = True
                        Texto = Texto + "@"
                    End If

                    Select Case item.idRubro
                        Case "L"
                            importeLiquido = importeLiquido + item.importe
                            If id_glo_sociedad = 7000 Then
                                Texto = Texto + " "
                                Texto = Texto + "0/" + Trim(item.cantidad)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((item.importe / item.cantidad), 2), 15)
                                '--- Omitir registro del descuento si es credito
                                Dim desto As Decimal = 0
                                If factura.condicion = "IL01" Then
                                    desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                                End If
                                desct = desct + desto           'Sumatoria del descuento
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                                totd = totd + item.importe      'sumatoria subtotal
                            Else
                                Texto = Texto + item.idRubro
                                Texto = Texto + "  " + Trim(item.trqt)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)


                                '--- Omitir registro del descuento si es credito
                                Dim desto As Decimal = 0
                                If factura.condicion = "IL01" Then
                                    desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                                End If
                                desct = desct + desto           'Sumatoria del descuento
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                                totd = totd + item.importe      'sumatoria subtotal
                            End If
                        Case "E"
                            importeEnvase = importeEnvase + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal

                        Case "C"
                            '--- Imprimir el nombre del material si no se ha hecho
                            If Not headPrinted Then printMaterialName(Texto, item)
                            importeCaja = importeCaja + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal
                    End Select

                    If i = times Then
                        times = times + 3
                        Texto = Texto + "@"
                    End If
                    'Se agrego corte
                    Texto = Texto + "@"
                Next

                '--- Pie de la factura
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(factura.importe, 2), 15) + vbCrLf
                Texto = Texto + "@"
                Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(totd, 2), 15) + vbCrLf
                Texto = Texto + "L I Q U I D O                           " + .AlinDer(FormatCurrency(importeLiquido, 2), 15) + vbCrLf
                Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
                Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Texto = Texto + "@"
                If factura.importeDesto <> 0 Then
                    'Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(factura.importeDesto * -1, 2), 15) + vbCrLf
                    Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(desct * -1, 2), 15) + vbCrLf
                End If
                'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importe) + objUtil.isDecimal(factura.importeDesto), 2), 15) + vbCrLf
                Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(totd) + objUtil.isDecimal(desct), 2), 15) + vbCrLf
                Texto = Texto + "F O R M A   P A G O                     " + vbCrLf
                Texto = Texto + "@"
                '--- Formas de pago
                For i As Integer = 0 To dtPagos.Rows.Count - 1
                    Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                    Texto = Texto + dtPagos.Rows(i).Item("Descripcion") & vbTab & "(+)"
                    Texto = Texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                    If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                        pieFactura = objimpresion.AlinCent("UD. TIENE " + Trim(objUtil.getDataValue("CPAGO", factura.condicion)) + " DIAS DE CREDITO AUTORIZADOS.", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("@ME COMPROMETO A CANCELARLA EL ", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent(Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("SI USTED PAGA A MAS TARDAR EL " + FormatDateTime(factura.fechaVence, DateFormat.ShortDate).ToString(), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("TIENE DERECHO A UN DESCUENTO DE " + FormatCurrency(objUtil.isDecimal(factura.importeDestoPP) * -1, 2), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("SIEMPRE Y CUANDO  NO DEVUELVA PRODUCTO.", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("RECIBI DE CONFORMIDAD LOS PRODUCTOS", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("DETALLADOS EN ESTA FACTURA CAMBIARIA.", mAncho) + vbCrLf
                        isCredito = True
                    End If
                Next
                Texto = Texto + "ENVASE  " & vbTab & "(-)" + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importeDestoEnv), 2), 44) + vbCrLf
                Texto = Texto + vbCrLf
                Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Admin: " + factura.serie + "-" + factura.numero, mAncho) + vbCrLf
                Texto = Texto + .AlinCent("Sujeto a pagos trimestrales", mAncho) + vbCrLf
                Texto = Texto + .AlinCent("AGENTE DE RETENCION DEL IVA", mAncho) + vbCrLf

                Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
                Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf

                Texto = Texto + pieFactura
                Texto = Texto + vbCrLf
                Texto = Texto + vbCrLf


                texto_1 = Texto
                texto_2 = Texto

            End With

            '--- Crear la copia en archivo de texto
            textToFile("FACT_" + factura.serie + factura.numero + ".txt", Texto)

            If preguntaSiImprimir Then
                If Not objimpresion.ConfirmaImpresion("Factura") Then
                    objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                    Return False
                End If
            End If

            texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
            texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
            texto_1 = texto_1 + vbCrLf
            texto_1 = texto_1 + vbCrLf


            Dim arreglo As String()
            arreglo = texto_1.ToString.Split("@")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
                texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                texto_2 = texto_2 + vbCrLf
                texto_2 = texto_2 + vbCrLf

                If isCredito Then
                    arreglo = texto_2.ToString.Split("@")
                    objimpresion.Imprime_Documento_grande(arreglo)
                End If
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 1)
                Return True
            Else
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                Return False
            End If
        Else
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
            Return False
        End If



    End Function

    Public Function imprimeFacturaLevuni(ByVal idFactura As String, ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim desct As Decimal = 0            'Variable que permite llevar la sumatoria de descuento
        Dim totd As Decimal = 0             'Variable que permite llevar la sumatoria del total del documento
        Dim Texto As String = ""            'Texto para la impresión 
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtFactura As DataTable
        Dim dtPagos As DataTable
        Dim importeCaja, importeEnvase, importeLiquido As Decimal
        Dim pieFactura As String = ""
        Dim isCredito As Boolean = False
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim headPrinted As Boolean = False

        dtFactura = objDocumento.getFacturaDetalle(idFactura)
        factura = objDocumento.getFactura(idFactura)
        cliente = objCliente.getDetalleDelCliente(factura.idCliente)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        Ruta = objRuta.getActiva()


        For i As Integer = 0 To dtPagos.Rows.Count - 1
            If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                isCredito = True
            End If
        Next

        If (Len(factura.preimpreso) > 0) Then
            With objimpresion
                objimpresion.Imprime_Encabezado_doc(Texto)
                Texto = Texto + objimpresion.AlinCent(" DOCUMENTO TRIBUTARIO ELECTRONICO ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("NO.: " + Trim(factura.noResolucion) + " DE FECHA: " + FormatDateTime(factura.fechaResolucion, DateFormat.ShortDate), mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("Serie: " + factura.serie + "  Del " + factura.inicial + " al " + factura.final, mAncho) + vbCrLf
                If (isCredito = True) Then
                    Texto = Texto + objimpresion.AlinCent("F A C T U R A   C A M B I A R I A - LIBRE  DE  PROTESTO", mAncho) + vbCrLf
                Else
                    Texto = Texto + objimpresion.AlinCent(" F  A  C  T  U  R  A  ", mAncho) + vbCrLf
                End If

                'Texto = Texto + objimpresion.AlinCent(" DOCUMENTO EN CONTINGENCIA  ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("NUMERO DE ACCESO: 846900589", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("obtenga el DTE Certificado en el sitio ", mAncho) + vbCrLf
                'Texto = Texto + objimpresion.AlinCent("www.sat.gob.gt/efactura", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("SERIE: " + factura.serieFEL + " NUMERO: " + factura.preimpreso, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Autorizacion: " + factura.UUID, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("", mAncho) + vbCrLf

                Texto = Texto + "LUGAR DE CREACION: " + objimpresion.partir_textoI(LTrim(RTrim(cliente.direccion)), 35) + vbCrLf
                Texto = Texto + "FECHA     : " + Format(CDate(factura.fechaEmision), "dd/MM/yyyy") & vbCrLf
                If Ruta.clienteGenerico = cliente.codigo Then
                    If (factura.ttipo = "ZTAE") Then
                        'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                        Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                        If factura.tipoReceptor = 4 Then
                            Texto = Texto + "NIT       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        Else
                        End If
                        If factura.tipoReceptor = 2 Then
                            Texto = Texto + "CUI       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        End If
                        If factura.tipoReceptor = 3 Then
                            Texto = Texto + "Pasaporte       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                        End If
                        Texto = Texto + "NEGOCIO   :____________________________________________" + vbCrLf
                    Else
                        'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                        Texto = Texto + "NOMBRE    : CONSUMIDOR  FINAL" + vbCrLf
                        Texto = Texto + "NIT       : C / F " + vbCrLf
                        Texto = Texto + "NEGOCIO   : CONSUMIDOR  FINAL" + vbCrLf
                    End If
                    Texto = Texto & "NOMBRE    : " & .AlinIzq(factura.nombre_fel, 50) & vbCrLf
                Else
                    Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    If factura.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    Else
                    End If
                    If factura.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    End If
                    If factura.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(factura.idreceptor, 50) + vbCrLf
                    End If
                    Texto = Texto & "NOMBRE    : " & .AlinIzq(factura.nombre_fel, 50) & vbCrLf
                    Texto = Texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
                End If
                Dim msg = "Desea imprimir la dirección "
                Dim style = MsgBoxStyle.YesNo
                Dim response = MsgBox(msg, style, "xoMobile")
                If response = MsgBoxResult.Yes Then
                    Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
                End If

                Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Texto = Texto + "COD.        PRODUCTO " + vbCrLf
                Texto = Texto + "C.U.        PRECIO           DESCUENTO        IMPORTE" + vbCrLf
                Texto = Texto + vbCrLf
                Texto = Texto + "@"
                '---Detalle de la factura
                For i As Integer = 0 To dtFactura.Rows.Count - 1

                    item = objDocumento.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    item.iva = co_glo_porcentajeIVA

                    If item.idRubro = "L" Or (factura.ttipo = "ZTAP" Or factura.ttipo = "ZTAE") Then
                        Texto = Texto + "-------------------------------------------------------" + vbCrLf
                        Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                        Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                        headPrinted = True
                        Texto = Texto + "@"
                    End If

                    Select Case item.idRubro
                        Case "L"

                            If id_glo_sociedad = 7000 Then
                                importeLiquido = importeLiquido + item.importe
                                'Texto = Texto + "UN"
                                Texto = Texto + " 0/" + Trim(item.cantidad)
                                'Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((item.importe / item.cantidad), 4), 10)


                                '--- Omitir registro del descuento si es credito
                                Dim desto As Decimal = 0
                                If factura.condicion = "IL01" Then
                                    desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                                End If
                                desct = desct + desto           'Sumatoria del descuento
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                                totd = totd + item.importe      'sumatoria subtotal
                            Else
                                importeLiquido = importeLiquido + item.importe
                                'Texto = Texto + "UN"
                                Texto = Texto + "  0/" + Trim(item.cantidad)
                                'Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((item.importe / item.cantidad), 4), 10)


                                '--- Omitir registro del descuento si es credito
                                Dim desto As Decimal = 0
                                If factura.condicion = "IL01" Then
                                    desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                                End If
                                desct = desct + desto           'Sumatoria del descuento
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                                Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                                totd = totd + item.importe      'sumatoria subtotal
                            End If

                            

                        Case "E"
                            importeEnvase = importeEnvase + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal

                        Case "C"
                            '--- Imprimir el nombre del material si no se ha hecho
                            If Not headPrinted Then printMaterialName(Texto, item)
                            importeCaja = importeCaja + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal
                    End Select

                    If i = times Then
                        times = times + 3
                        Texto = Texto + "@"
                    End If
                    'Se agrego corte
                    Texto = Texto + "@"
                Next

                '--- Pie de la factura
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(factura.importe, 2), 15) + vbCrLf
                Texto = Texto + "@"
                Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(totd, 2), 15) + vbCrLf
                Texto = Texto + "L I Q U I D O                           " + .AlinDer(FormatCurrency(importeLiquido, 2), 15) + vbCrLf
                Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
                Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Texto = Texto + "@"
                If factura.importeDesto <> 0 Then
                    'Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(factura.importeDesto * -1, 2), 15) + vbCrLf
                    Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(desct * -1, 2), 15) + vbCrLf
                End If
                'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importe) + objUtil.isDecimal(factura.importeDesto), 2), 15) + vbCrLf
                Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(totd) + objUtil.isDecimal(desct), 2), 15) + vbCrLf
                Texto = Texto + "F O R M A   P A G O                     " + vbCrLf
                Texto = Texto + "@"
                '--- Formas de pago
                For i As Integer = 0 To dtPagos.Rows.Count - 1
                    Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                    Texto = Texto + dtPagos.Rows(i).Item("Descripcion") & vbTab & "(+)"
                    Texto = Texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                    If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                        pieFactura = objimpresion.AlinCent("UD. TIENE " + Trim(objUtil.getDataValue("CPAGO", factura.condicion)) + " DIAS DE CREDITO AUTORIZADOS.", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("@ME COMPROMETO A CANCELARLA EL ", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent(Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("SI USTED PAGA A MAS TARDAR EL " + FormatDateTime(factura.fechaVence, DateFormat.ShortDate).ToString(), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("TIENE DERECHO A UN DESCUENTO DE " + FormatCurrency(objUtil.isDecimal(factura.importeDestoPP) * -1, 2), mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("SIEMPRE Y CUANDO  NO DEVUELVA PRODUCTO.", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("RECIBI DE CONFORMIDAD LOS PRODUCTOS", mAncho) + vbCrLf
                        pieFactura = pieFactura + objimpresion.AlinCent("DETALLADOS EN ESTA FACTURA CAMBIARIA.", mAncho) + vbCrLf
                        isCredito = True
                    End If
                Next
                Texto = Texto + "ENVASE  " & vbTab & "(-)" + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importeDestoEnv), 2), 44) + vbCrLf
                Texto = Texto + vbCrLf
                Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Admin: " + factura.serie + "-" + factura.numero, mAncho) + vbCrLf
                Texto = Texto + .AlinCent("Sujeto a pagos trimestrales", mAncho) + vbCrLf
                Texto = Texto + .AlinCent("AGENTE DE RETENCION DEL IVA", mAncho) + vbCrLf

                Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
                Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf

                Texto = Texto + pieFactura
                Texto = Texto + vbCrLf
                Texto = Texto + vbCrLf


                texto_1 = Texto
                texto_2 = Texto

            End With

            '--- Crear la copia en archivo de texto
            textToFile("FACT_" + factura.serie + factura.numero + ".txt", Texto)

            If preguntaSiImprimir Then
                If Not objimpresion.ConfirmaImpresion("Factura") Then
                    objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                    Return False
                End If
            End If

            texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
            texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
            texto_1 = texto_1 + vbCrLf
            texto_1 = texto_1 + vbCrLf


            Dim arreglo As String()
            arreglo = texto_1.ToString.Split("@")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
                texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                texto_2 = texto_2 + vbCrLf
                texto_2 = texto_2 + vbCrLf

                If isCredito Then
                    arreglo = texto_2.ToString.Split("@")
                    objimpresion.Imprime_Documento_grande(arreglo)
                End If
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 1)
                Return True
            Else
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                Return False
            End If
        Else
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
            Return False
        End If



    End Function

    Public Function imprimeFacturaSINFEL(ByVal idFactura As String, ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim desct As Decimal = 0            'Variable que permite llevar la sumatoria de descuento
        Dim totd As Decimal = 0             'Variable que permite llevar la sumatoria del total del documento
        Dim Texto As String = ""            'Texto para la impresión 
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtFactura As DataTable
        Dim dtPagos As DataTable
        Dim importeCaja, importeEnvase, importeLiquido As Decimal
        Dim pieFactura As String = ""
        Dim isCredito As Boolean = False
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim headPrinted As Boolean = False

        dtFactura = objDocumento.getFacturaDetalle(idFactura)
        factura = objDocumento.getFactura(idFactura)
        cliente = objCliente.getDetalleDelCliente(factura.idCliente)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        Ruta = objRuta.getActiva()

        With objimpresion

            objimpresion.Imprime_Encabezado_doc(Texto)
            Texto = Texto + objimpresion.AlinCent(" AUTORIZADA SEGUN RESOLUCION ", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("NO.: " + Trim(factura.noResolucion) + " DE FECHA: " + FormatDateTime(factura.fechaResolucion, DateFormat.ShortDate), mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("Serie: " + factura.serie + "  Del " + factura.inicial + " al " + factura.final, mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("F A C T U R A   C A M B I A R I A", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("LIBRE  DE  PROTESTO", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("SERIE: " + factura.serie + " No.: " + factura.numero, mAncho) + vbCrLf
            Texto = Texto + "LUGAR DE CREACION: " + objimpresion.partir_textoI(LTrim(RTrim(cliente.direccion)), 35) + vbCrLf
            Texto = Texto + "FECHA     : " + Format(CDate(factura.fechaEmision), "dd/MM/yyyy") & vbCrLf
            If Ruta.clienteGenerico = cliente.codigo Then
                If (factura.ttipo = "ZTAE") Then
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                    If factura.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    End If
                    If factura.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(cliente.numeroDi, 50) + vbCrLf
                    End If

                    If factura.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    End If

                    Texto = Texto + "NEGOCIO   :____________________________________________" + vbCrLf
                Else
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : CONSUMIDOR  FINAL" + vbCrLf
                    Texto = Texto + "NIT       : C / F " + vbCrLf
                    Texto = Texto + "NEGOCIO   : CONSUMIDOR  FINAL" + vbCrLf
                End If
            Else
                Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                Texto = Texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
            End If

            Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
            Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "COD.        PRODUCTO " + vbCrLf
            Texto = Texto + "C.U.        PRECIO           DESCUENTO        IMPORTE" + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + "@"
            '---Detalle de la factura
            For i As Integer = 0 To dtFactura.Rows.Count - 1

                item = objDocumento.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                item.iva = co_glo_porcentajeIVA

                If item.idRubro = "L" Or (factura.ttipo = "ZTAP" Or factura.ttipo = "ZTAE") Then
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                    Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                    headPrinted = True
                    Texto = Texto + "@"
                End If

                Select Case item.idRubro
                    Case "L"
                        If id_glo_sociedad = 7000 Then

                            importeLiquido = importeLiquido + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "    0/" + Trim(item.cantidad)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)


                            '--- Omitir registro del descuento si es credito
                            Dim desto As Decimal = 0
                            If factura.condicion = "IL01" Then
                                desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                            End If
                            desct = desct + desto           'Sumatoria del descuento
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal

                        Else
                            importeLiquido = importeLiquido + item.importe
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)


                            '--- Omitir registro del descuento si es credito
                            Dim desto As Decimal = 0
                            If factura.condicion = "IL01" Then
                                desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                            End If
                            desct = desct + desto           'Sumatoria del descuento
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                            Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                            totd = totd + item.importe      'sumatoria subtotal

                        End If
                        

                    Case "E"
                        importeEnvase = importeEnvase + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal

                    Case "C"
                        '--- Imprimir el nombre del material si no se ha hecho
                        If Not headPrinted Then printMaterialName(Texto, item)
                        importeCaja = importeCaja + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal
                End Select

                If i = times Then
                    times = times + 3
                    Texto = Texto + "@"
                End If
                'Se agrego corte
                Texto = Texto + "@"
            Next

            '--- Pie de la factura
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(factura.importe, 2), 15) + vbCrLf
            Texto = Texto + "@"
            Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(totd, 2), 15) + vbCrLf
            Texto = Texto + "L I Q U I D O                           " + .AlinDer(FormatCurrency(importeLiquido, 2), 15) + vbCrLf
            Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
            Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "@"
            If factura.importeDesto <> 0 Then
                'Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(factura.importeDesto * -1, 2), 15) + vbCrLf
                Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(desct * -1, 2), 15) + vbCrLf
            End If
            'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importe) + objUtil.isDecimal(factura.importeDesto), 2), 15) + vbCrLf
            Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(totd) + objUtil.isDecimal(desct), 2), 15) + vbCrLf
            Texto = Texto + "F O R M A   P A G O                     " + vbCrLf
            Texto = Texto + "@"
            '--- Formas de pago
            For i As Integer = 0 To dtPagos.Rows.Count - 1
                Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                Texto = Texto + dtPagos.Rows(i).Item("Descripcion") & vbTab & "(+)"
                Texto = Texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                    pieFactura = objimpresion.AlinCent("UD. TIENE " + Trim(objUtil.getDataValue("CPAGO", factura.condicion)) + " DIAS DE CREDITO AUTORIZADOS.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("@ME COMPROMETO A CANCELARLA EL ", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent(Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SI USTED PAGA A MAS TARDAR EL " + Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("TIENE DERECHO A UN DESCUENTO DE " + FormatCurrency(objUtil.isDecimal(factura.importeDestoPP) * -1, 2), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SIEMPRE Y CUANDO  NO DEVUELVA PRODUCTO.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("RECIBI DE CONFORMIDAD LOS PRODUCTOS", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("DETALLADOS EN ESTA FACTURA CAMBIARIA.", mAncho) + vbCrLf
                    isCredito = True
                End If
            Next
            Texto = Texto + "ENVASE  " & vbTab & "(-)" + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importeDestoEnv), 2), 44) + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
            Texto = Texto + .AlinCent("Sujeto a pagos trimestrales", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("AGENTE DE RETENCION DEL IVA", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf
            Texto = Texto + pieFactura
            Texto = Texto + vbCrLf
            Texto = Texto + vbCrLf


            texto_1 = Texto
            texto_2 = Texto

        End With

        '--- Crear la copia en archivo de texto
        textToFile("FACT_" + factura.serie + factura.numero + ".txt", Texto)

        If preguntaSiImprimir Then
            If Not objimpresion.ConfirmaImpresion("Factura") Then
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                Return False
            End If
        End If

        texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
        texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
        texto_1 = texto_1 + vbCrLf
        texto_1 = texto_1 + vbCrLf


        Dim arreglo As String()
        arreglo = texto_1.ToString.Split("@")
        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
            texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
            texto_2 = texto_2 + vbCrLf
            texto_2 = texto_2 + vbCrLf

            If isCredito Then
                arreglo = texto_2.ToString.Split("@")
                objimpresion.Imprime_Documento_grande(arreglo)
            End If
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 1)
            Return True
        Else
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
            Return False
        End If
    End Function

    Public Function imprimeCambio(ByVal idFactura As String, ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim desct As Decimal = 0            'Variable que permite llevar la sumatoria de descuento
        Dim totd As Decimal = 0             'Variable que permite llevar la sumatoria del total del documento
        Dim Texto As String = ""            'Texto para la impresión 
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtFactura As DataTable
        Dim dtPagos As DataTable
        Dim importeCaja, importeEnvase, importeLiquido As Decimal
        Dim pieFactura As String = ""
        Dim isCredito As Boolean = False
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim headPrinted As Boolean = False



        dtFactura = objDocumento.getFacturaDetalle(idFactura)
        factura = objDocumento.getFactura(idFactura)
        cliente = objCliente.getDetalleDelCliente(factura.idCliente)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        Ruta = objRuta.getActiva()

        With objimpresion
            objimpresion.Imprime_Encabezado_doc(Texto)
            Texto = Texto + objimpresion.AlinCent("Serie: " + factura.serie + "  Del " + factura.inicial + " al " + factura.final, mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("D O C U M E N T O   C A M B I O", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("SERIE: " + factura.serie + " No.: " + factura.numero, mAncho) + vbCrLf
            Texto = Texto + "LUGAR DE CREACION: " + objimpresion.partir_textoI(LTrim(RTrim(cliente.direccion)), 35) + vbCrLf
            Texto = Texto + "FECHA     : " + Format(CDate(factura.fechaEmision), "dd/MM/yyyy") & vbCrLf
            If Ruta.clienteGenerico = cliente.codigo Then
                If (factura.ttipo = "ZTAE") Then
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    Texto = Texto + "NEGOCIO   :____________________________________________" + vbCrLf
                Else
                    'Texto = Texto + "NOMBRE    :____________________________________________" + vbCrLf
                    Texto = Texto + "NOMBRE    : CONSUMIDOR  FINAL" + vbCrLf
                    Texto = Texto + "NIT       : C / F " + vbCrLf
                    Texto = Texto + "NEGOCIO   : CONSUMIDOR  FINAL" + vbCrLf
                End If
            Else
                Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                Texto = Texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
            End If

            Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
            Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "COD.        PRODUCTO " + vbCrLf
            Texto = Texto + "@"
            Texto = Texto + "C.U.        PRECIO           DESCUENTO        IMPORTE" + vbCrLf
            Texto = Texto + "@"
            '---Detalle de la factura
            For i As Integer = 0 To dtFactura.Rows.Count - 1

                item = objDocumento.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                item.iva = co_glo_porcentajeIVA

                If item.idRubro = "L" Or (factura.ttipo = "ZTAP" Or factura.ttipo = "ZTAE") Then
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                    Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                    headPrinted = True
                    Texto = Texto + "@"
                End If

                Select Case item.idRubro
                    Case "L"
                        importeLiquido = importeLiquido + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)


                        '--- Omitir registro del descuento si es credito
                        Dim desto As Decimal = 0
                        If factura.condicion = "IL01" Then
                            desto = objUtil.isDecimal(item.importeDesto) + objUtil.isDecimal(item.valorIvaDesto)
                        End If
                        desct = desct + desto           'Sumatoria del descuento
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency((desto) * -1, 2), 18)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 16) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal

                    Case "E"
                        importeEnvase = importeEnvase + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal

                    Case "C"
                        '--- Imprimir el nombre del material si no se ha hecho
                        If Not headPrinted Then printMaterialName(Texto, item)
                        importeCaja = importeCaja + item.importe
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.precio, 2), 15)
                        Texto = Texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 34) + vbCrLf
                        totd = totd + item.importe      'sumatoria subtotal
                End Select

                If i = times Then
                    times = times + 3
                    Texto = Texto + "@"
                End If
            Next

            '--- Pie de la factura
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(factura.importe, 2), 15) + vbCrLf
            Texto = Texto + "@"
            'Texto = Texto + "S U B T O T A L                         " + .AlinDer(FormatCurrency(totd, 2), 15) + vbCrLf
            'Texto = Texto + "L I Q U I D O                           " + .AlinDer(FormatCurrency(importeLiquido, 2), 15) + vbCrLf
            'Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
            'Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "@"
            If factura.importeDesto <> 0 Then
                'Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(factura.importeDesto * -1, 2), 15) + vbCrLf
                Texto = Texto + "DESCUENTO(-)                            " + .AlinDer(FormatCurrency(desct * -1, 2), 15) + vbCrLf
            End If
            'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importe) + objUtil.isDecimal(factura.importeDesto), 2), 15) + vbCrLf
            'Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(objUtil.isDecimal(totd) + objUtil.isDecimal(desct), 2), 15) + vbCrLf
            'Texto = Texto + "F O R M A   P A G O                     " + vbCrLf
            Texto = Texto + "@"
            '--- Formas de pago
            For i As Integer = 0 To dtPagos.Rows.Count - 1
                Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                Texto = Texto + dtPagos.Rows(i).Item("Descripcion") & vbTab & "(+)"
                Texto = Texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                If dtPagos.Rows(i).Item("idviapago") = "CR" Then
                    pieFactura = objimpresion.AlinCent("UD. TIENE " + Trim(objUtil.getDataValue("CPAGO", factura.condicion)) + " DIAS DE CREDITO AUTORIZADOS.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("@ME COMPROMETO A CANCELARLA EL ", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent(Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SI USTED PAGA A MAS TARDAR EL " + Format(CDate(factura.fechaVence), "dd/MM/yyyy"), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("TIENE DERECHO A UN DESCUENTO DE " + FormatCurrency(objUtil.isDecimal(factura.importeDestoPP) * -1, 2), mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("SIEMPRE Y CUANDO  NO DEVUELVA PRODUCTO.", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("RECIBI DE CONFORMIDAD LOS PRODUCTOS", mAncho) + vbCrLf
                    pieFactura = pieFactura + objimpresion.AlinCent("DETALLADOS EN ESTA FACTURA CAMBIARIA.", mAncho) + vbCrLf
                    isCredito = True
                End If
            Next
            'Texto = Texto + "ENVASE  " & vbTab & "(-)" + .AlinDer(FormatCurrency(objUtil.isDecimal(factura.importeDestoEnv), 2), 44) + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf

            Texto = Texto + pieFactura
            Texto = Texto + vbCrLf
            Texto = Texto + vbCrLf

            texto_1 = Texto
            texto_2 = Texto

        End With

        '--- Crear la copia en archivo de texto
        textToFile("CAM_" + factura.serie + factura.numero + ".txt", Texto)

        If preguntaSiImprimir Then
            If Not objimpresion.ConfirmaImpresion("Factura") Then
                objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
                Return False
            End If
        End If

        texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
        texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
        texto_1 = texto_1 + vbCrLf
        texto_1 = texto_1 + vbCrLf


        Dim arreglo As String()
        arreglo = texto_1.ToString.Split("@")
        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
            texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
            texto_2 = texto_2 + vbCrLf
            texto_2 = texto_2 + vbCrLf

            If isCredito Then
                arreglo = texto_2.ToString.Split("@")
                objimpresion.Imprime_Documento_grande(arreglo)
            End If
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 1)
            Return True
        Else
            objDocumento.numeroImpresiones(idFactura, "FACTURA", 0)
            Return False
        End If
    End Function

    Private Sub printMaterialName(ByRef Texto As String, ByVal item As ItemCO)
        Texto = Texto + "-------------------------------------------------------" + vbCrLf
        Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
        Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
    End Sub

    Public Function imprimeRecibo(ByVal idRecibo As String, ByVal preguntaSiImprimir As Boolean) As Boolean
        Dim texto As String = ""        'Texto para la impresión
        Dim textoOriginal As String
        Dim textoCopia As String

        Dim recibo As New documentoCO
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtRecibo As DataTable
        Dim dtPagos As DataTable
        Dim pagos As Decimal = 0
        Dim saldo As Decimal = 0
        Dim detener As Boolean = True
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL

        Ruta = objRuta.getActiva()
        recibo = objDocumento.getRecibo(idRecibo)
        dtRecibo = objDocumento.getReciboDetalle(idRecibo)
        dtPagos = objDocumento.getReciboPagos(idRecibo)
        cliente = objCliente.getDetalleDelCliente(recibo.idCliente)
        Ruta = objRuta.getActiva()

        '-- Verificar si el pago es credito al 100%
        For i As Integer = 0 To dtPagos.Rows.Count - 1
            If dtPagos.Rows(i).Item("idviapago") <> "CR" Then
                detener = False
            End If
        Next
        If dtPagos.Rows.Count = 0 Then
            detener = False
        End If

        If detener Then
            objDocumento.numeroImpresiones(recibo.idEncabezado, "RECIBO", 1)
            Return True
        End If

        With objimpresion
            '--- Encabezado
            texto = texto + vbCrLf
            texto = texto + vbCrLf
            texto = texto + vbCrLf
            texto = texto + "@"
            objimpresion.Imprime_Encabezado_doc(texto)

            texto = texto + .AlinCent("RECIBO DE CAJA: " & Trim(recibo.serie) & "-" & Trim(recibo.numero), mAncho) & vbCrLf
            texto = texto + "FECHA     : " + Format(CDate(recibo.fechaEmision), "dd/MM/yyyy") & vbCrLf
            If Ruta.clienteGenerico = cliente.codigo Then
                If (recibo.doTipo = "ZTAE") Then
                    texto = texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    'texto = texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    texto = texto + "NEGOCIO   : --N/A-- " + vbCrLf
                Else
                    texto = texto + "NOMBRE    : CONSUMIDOR FINAL " + vbCrLf
                    'texto = texto + "NIT       : C/F " + vbCrLf
                    texto = texto + "NEGOCIO   : CONSUMIDOR FINAL " + vbCrLf
                End If
                

            Else
                texto = texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                'texto = texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                texto = texto + "NEGOCIO   : " + objimpresion.partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
            End If
            texto = texto + "DIRECCION : " + Trim(cliente.direccion) + vbCrLf
            texto = texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
            texto = texto + "-------------------------------------------------------" + vbCrLf
            texto = texto + "FACTURA          VALOR             PAGOS          SALDO" + vbCrLf
            texto = texto + "-------------------------------------------------------" + vbCrLf

            '--- Detalle
            For i As Integer = 0 To dtRecibo.Rows.Count - 1
                item = objDocumento.getReciboByItem(dtRecibo.Rows(i))
                texto = texto + Trim(item.idProducto) + "-" + Trim(item.descripcion) & vbCrLf
                texto = texto + objimpresion.AlinDer(FormatCurrency(recibo.importe, 2), 25)
                texto = texto + objimpresion.AlinDer(FormatCurrency(item.importe, 2), 15)
                texto = texto + objimpresion.AlinDer(FormatCurrency(item.saldo, 2), 15) + vbCrLf
                saldo = saldo + item.saldo
            Next
            texto = texto + "-------------------------------------------------------" + vbCrLf

            '--- Formas de pago       
            For i As Integer = 0 To dtPagos.Rows.Count - 1
                Dim a As Integer = Len(dtPagos.Rows(i).Item("Descripcion") + "(+)") 'Ancho Variable
                texto = texto + dtPagos.Rows(i).Item("Descripcion") + "(+)"
                texto = texto + objimpresion.AlinDer(FormatCurrency(dtPagos.Rows(i).Item("importe"), 2), mAncho - (a)) + vbCrLf
                texto = texto + "@"
                '--- Acumular el monto total pagado.
                If Not dtPagos.Rows(i).Item("idviapago") = "CR" Then
                    pagos = pagos + dtPagos.Rows(i).Item("importe")
                End If
            Next
            texto = texto + "ENVASE   " & vbTab & "(-)" + .AlinDer(FormatCurrency(recibo.importeDestoEnv, 2), 43) + vbCrLf
            texto = texto + "@"
            'Descuento total
            If recibo.importeDesto <> 0 Then
                texto = texto + "DESCUENTO" & vbTab & "(-)" + .AlinDer(FormatCurrency(recibo.importeDesto, 2), 43) + vbCrLf
            End If
            texto = texto + "-------------------------------------------------------" + vbCrLf
            texto = texto + "@" + vbCrLf
            texto = texto + "PAGO RECIBIDO"
            texto = texto + objimpresion.AlinDer(FormatCurrency(pagos, 2), 42) & vbCrLf
            texto &= .AlinCent("---------------------------------------------", mAncho) + vbCrLf
            texto = texto + vbCrLf
            texto = texto + vbCrLf
            texto = texto + vbCrLf
            texto = texto + "@" + vbCrLf
            textoOriginal = texto
            textoCopia = texto


            '--- Crear la copia en archivo de texto
            textToFile("REC_" + recibo.serie + recibo.numero + ".txt", texto)

            If preguntaSiImprimir Then
                If Not objimpresion.ConfirmaImpresion("Recibo") Then
                    objDocumento.numeroImpresiones(recibo.idEncabezado, "RECIBO", 0)
                    Return False
                End If
            End If

            '---- Primera impresion
            textoOriginal &= "      FIRMA CLIENTE      [COPIA CLIENTE]    " + vbCrLf
            textoOriginal &= .AlinCent(co_glo_vendedor, mAncho) + vbCrLf
            textoOriginal &= vbCrLf
            textoOriginal &= vbCrLf
            Dim arreglo As String()
            Dim arreglo2 As String()
            arreglo = textoOriginal.ToString.Split("@")
            arreglo2 = textoCopia.ToString.Split("@")
            
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                textoCopia &= vbCrLf
                textoCopia &= vbCrLf
                textoCopia &= "@" + vbCrLf
                textoCopia &= "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                textoCopia &= vbCrLf
                textoCopia &= vbCrLf
                objimpresion.Imprime_Documento_grande(arreglo2)
                objDocumento.numeroImpresiones(recibo.idEncabezado, "RECIBO", 1)
                Return True
            Else
                objDocumento.numeroImpresiones(recibo.idEncabezado, "RECIBO", 0)
                Return False
            End If
        End With
    End Function

    Public Function ImprimeNotaCredito(ByVal idNc As String, ByVal preguntaSiImprimir As Boolean, ByVal docAsociado As String) As Boolean
        Dim Texto As String = ""
        Dim objDocumentoBL As New DocumentoBL
        Dim origen As New documentoCO      'Origen Factura
        Dim nc As New documentoCO
        Dim cliente As ClienteCO
        Dim dtFEL As DataTable
        Dim item As ItemCO
        Dim dtNc As DataTable
        Dim importeCaja, importeEnvase As Decimal
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim idproductoFl As String = ""
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL

        Ruta = objRuta.getActiva()

        dtNc = objDocumento.getNotaCreditoDetalle(idNc)
        nc = objDocumento.getNotaCredito(idNc)
        cliente = objCliente.getDetalleDelCliente(nc.idCliente)

        Dim serief As String = ""
        Dim numerof As String = ""

        If nc.idEncFacturaRelacionada <> 0 Then
            Try
                origen = objDocumentoBL.getFactura(nc.idEncFacturaRelacionada)
                serief = origen.serieFEL
                numerof = origen.preimpreso
            Catch ex As Exception
                serief = ""
                numerof = ""
            End Try
        Else
            Try
                dtFEL = objDocumentoBL.getCXCFEL(nc.idReciboRelacionado)
                serief = dtFEL.Rows(0).Item("seriefel")
                numerof = dtFEL.Rows(0).Item("numeroautorizacion")
            Catch ex As Exception
                serief = ""
                numerof = ""
            End Try
        End If


        If (Len(nc.preimpreso) > 0) Then
            With objimpresion
                .Imprime_Encabezado_doc(Texto)
                Texto = Texto + objimpresion.AlinCent("N O T A  D E  C R E D I T O", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("SERIE: " + nc.serieFEL + " NUMERO: " + nc.preimpreso, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Numero Autorizacion: " + nc.UUID, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("FACT. ASOCIADA: " + serief + " - " + numerof, mAncho) + vbCrLf

                '--- Tipo de nota de credito
                Select Case Trim(nc.ttipo)
                    Case 4
                        Texto = Texto + .AlinCent("P O R  E N V A S E  R E C I B I D O", mAncho) + vbCrLf
                    Case 5
                        Texto = Texto + .AlinCent("POR DESCUENTO PRONTO PAGO", mAncho) + vbCrLf
                End Select
                Texto = Texto + "FECHA     : " + Format(CDate(nc.fechaEmision), "dd/MM/yyyy") & vbCrLf
                If Ruta.clienteGenerico = cliente.codigo Then
                    If (nc.doTipo = "ZTAE") Then
                        Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                        If nc.tipoReceptor = 4 Then
                            Texto = Texto + "NIT       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        Else
                        End If
                        If nc.tipoReceptor = 2 Then
                            Texto = Texto + "CUI       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        End If
                        If nc.tipoReceptor = 3 Then
                            Texto = Texto + "Pasaporte       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        End If
                        Texto = Texto & "NOMBRE    : " & .AlinIzq(nc.nombre_fel, 50) & vbCrLf
                        Texto = Texto + "NEGOCIO   : --N/A-- " + vbCrLf
                    Else
                        Texto = Texto + "NOMBRE    : CONSUMIDOR FINAL " + vbCrLf
                        Texto = Texto + "NIT       : C/F " + vbCrLf
                        Texto = Texto + "NEGOCIO   : CONSUMIDOR FINAL " + vbCrLf
                    End If
                Else
                    Texto = Texto + "CLIENTE   : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    If nc.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    Else
                    End If
                    If nc.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    End If
                    If nc.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    End If
                    Texto = Texto & "NOMBRE    : " & .AlinIzq(nc.nombre_fel, 50) & vbCrLf
                    Texto = Texto + "NEGOCIO   : " + .partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
                End If
                'Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
                Dim msg = "DESEA IMPRIMIR DIRECCION NOTA CREDITO "
                Dim style = MsgBoxStyle.YesNo
                Dim response = MsgBox(msg, style, "xoMobile")
                If response = MsgBoxResult.Yes Then
                    Texto = Texto + "DIRECCION : " + Trim(nc.direccionfel) + vbCrLf
                End If
                Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Texto = Texto + "@"
                Select Case Trim(nc.ttipo)
                    Case 4
                        '--- Detalle Nota de credito por devolucion de envase
                        Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                        Texto = Texto + "C.U.                PRECIO                    IMPORTE" + vbCrLf

                        '---Detalle de la nc
                        For i As Integer = 0 To dtNc.Rows.Count - 1
                            item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                            If Not idproductoFl = item.litm.ToString Then
                                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                                Texto = Texto + "@"
                                Texto = Texto + objimpresion.AlinIzq(item.litm.ToString, 10)
                                Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                            End If
                            Select Case item.idRubro
                                Case "E"
                                    importeEnvase = importeEnvase + item.importe
                                Case "C"
                                    importeCaja = importeCaja + item.importe
                            End Select
                            Texto = Texto + item.idRubro
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + .AlinDer(FormatCurrency(item.precio * (1 + item.iva), 2), 20)
                            Texto = Texto + .AlinDer(FormatCurrency(item.importe, 2), 28) + vbCrLf
                            idproductoFl = item.litm.ToString
                            'Se agrego corte
                            Texto = Texto + "@"
                            If i = times Then
                                times = times + 5
                                Texto = Texto + "@"
                            End If

                        Next

                        Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    Case 5
                        '--- Detalle Nota de credito por Descuento Pronto Pago
                        Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                        Texto = Texto + "C.U.                %DESC                    DESCUENTO" + vbCrLf
                        Texto = Texto + "-------------------------------------------------------" + vbCrLf
                        '---Detalle de la nc
                        For i As Integer = 0 To dtNc.Rows.Count - 1
                            item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                            Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                            Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                            Texto = Texto + "  " + Trim(item.trqt)
                            Texto = Texto + .AlinDer(FormatPercent(item.porcentajeDesto / 100, 2), 21)
                            Texto = Texto + .AlinDer(FormatCurrency(item.importeDesto, 2), 28) + vbCrLf
                            Texto = Texto + "-------------------------------------------------------" + vbCrLf
                            Texto = Texto + "@"
                        Next

                End Select

                '--- Pie de la nc
                Select Case Trim(nc.ttipo)
                    Case 4
                        Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                        Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
                        Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
                    Case 5
                        Texto = Texto + "DESCUENTO TOTAL                         " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                        Texto = Texto + "D E S C U E N T O(-)                    " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                End Select

                'Texto = Texto + "No. FACTURA QUE ORIGINA ESTA N.C. " & docAsociado & " " & FormatDateTime(nc.fechaEmision.ToString, DateFormat.ShortDate) & vbCrLf
                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                'Texto = Texto + "N.C. AUTORIZADA SEGUN RESOLUCION " + vbCrLf
                
                'Texto = Texto + "NO.:" + Trim(nc.noResolucion) + "DE FECHA: " + FormatDateTime(nc.fechaResolucion, DateFormat.ShortDate) + vbCrLf
                'Texto = Texto + "Serie: " + nc.serie + "  Del " + nc.inicial + " al " + nc.final + vbCrLf

                Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
                Texto = Texto + objimpresion.AlinCent("Admin : " + nc.serie + "-" + nc.numero, mAncho) + vbCrLf
                Texto = Texto + "Sujeto a pagos trimestrales " + vbCrLf
                Texto = Texto + "AGENTE DE RETENCION DEL IVA " + vbCrLf
                Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
                Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf
                Texto = Texto + vbCrLf
                Texto = Texto + vbCrLf
                texto_1 = Texto
                texto_2 = Texto
                'Texto = Texto + "---------------------------------------------" + vbCrLf
                'Texto = Texto + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                'Texto = Texto + vbCrLf
                'Texto = Texto + vbCrLf

            End With

            '--- Crear la copia en archivo de texto
            Select Case Trim(nc.ttipo)
                Case 4
                    textToFile("NCE_" + nc.serie + nc.numero + ".txt", Texto)
                Case 5
                    textToFile("NCDP_" + nc.serie + nc.numero + ".txt", Texto)
            End Select

            If preguntaSiImprimir Then
                If Not objimpresion.ConfirmaImpresion("Nota de credito") Then
                    objDocumento.numeroImpresiones(idNc, "NC", 0)
                    Return False
                End If
            End If

            texto_1 = texto_1 + "@"
            texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
            texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
            texto_1 = texto_1 + vbCrLf
            texto_1 = texto_1 + vbCrLf

            Dim arreglo As String()
            arreglo = texto_1.ToString.Split("@")

            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                texto_2 = texto_2 + "@"
                texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
                texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                texto_2 = texto_2 + vbCrLf
                texto_2 = texto_2 + vbCrLf
                arreglo = texto_2.ToString.Split("@")
                objimpresion.Imprime_Documento_grande(arreglo)
                objDocumento.numeroImpresiones(idNc, "NC", 1)
                Return True
            Else
                objDocumento.numeroImpresiones(idNc, "NC", 0)
                Return False
            End If
        Else
            Return False
        End If
       
    End Function

    Public Function ImprimeNotaCreditoContingencia(ByVal idNc As String, ByVal preguntaSiImprimir As Boolean, ByVal docAsociado As String) As Boolean
        Dim Texto As String = ""
        Dim nc As New documentoCO
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtNc As DataTable
        Dim importeCaja, importeEnvase As Decimal
        Dim idproductoFl As String = ""
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL

        Ruta = objRuta.getActiva()

        dtNc = objDocumento.getNotaCreditoDetalle(idNc)
        nc = objDocumento.getNotaCredito(idNc)
        cliente = objCliente.getDetalleDelCliente(nc.idCliente)
        If (Len(nc.numeroacceso) > 0) Then
            With objimpresion
                .Imprime_Encabezado_doc(Texto)
                Texto = Texto + objimpresion.AlinCent(" DOCUMENTO TRIBUTARIO ELECTRONICO ", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent(" DOCUMENTO EN CONTINGENCIA ", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("N O T A  D E  C R E D I T O", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("NUMERO DE ACCESO: " & nc.numeroacceso, mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("obtenga el DTE Certificado en el sitio ", mAncho) + vbCrLf
                Texto = Texto + objimpresion.AlinCent("www.sat.gob.gt/efactura", mAncho) + vbCrLf

                '--- Tipo de nota de credito
                Select Case Trim(nc.ttipo)
                    Case 4
                        Texto = Texto + .AlinCent("P O R  E N V A S E  R E C I B I D O", mAncho) + vbCrLf
                    Case 5
                        Texto = Texto + .AlinCent("POR DESCUENTO PRONTO PAGO", mAncho) + vbCrLf
                End Select
                Texto = Texto + "FECHA     : " + Format(CDate(nc.fechaEmision), "dd/MM/yyyy") & vbCrLf
                If Ruta.clienteGenerico = cliente.codigo Then
                    If (nc.doTipo = "ZTAE") Then
                        Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                        'If nc.tipoReceptor = 4 Then
                        'Texto = Texto + "NIT       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        'Else
                        'End If
                        'If nc.tipoReceptor = 2 Then
                        'Texto = Texto + "CUI       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        'End If
                        'If nc.tipoReceptor = 3 Then
                        'Texto = Texto + "Pasaporte       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                        'End If
                        Texto = Texto + "NEGOCIO   : --N/A-- " + vbCrLf
                    Else
                        Texto = Texto + "NOMBRE    : CONSUMIDOR FINAL " + vbCrLf
                        Texto = Texto + "NIT       : C/F " + vbCrLf
                        Texto = Texto + "NEGOCIO   : CONSUMIDOR FINAL " + vbCrLf
                    End If

                Else
                    Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf

                    If nc.tipoReceptor = 4 Then
                        Texto = Texto + "NIT       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    Else
                    End If
                    If nc.tipoReceptor = 2 Then
                        Texto = Texto + "CUI       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    End If
                    If nc.tipoReceptor = 3 Then
                        Texto = Texto + "Pasaporte       : " + .AlinIzq(nc.idreceptor, 50) + vbCrLf
                    End If

                    Texto = Texto + "NEGOCIO   : " + .partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
                    End If
                    'Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
                    Dim msg = "DESEA IMPRIMIR DIRECCION NOTA CREDITO "
                    Dim style = MsgBoxStyle.YesNo
                    Dim response = MsgBox(msg, style, "xoMobile")
                    If response = MsgBoxResult.Yes Then
                    Texto = Texto + "DIRECCION : " + Trim(cliente.direccion) + vbCrLf
                    End If
                    Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    Texto = Texto + "@"
                    Select Case Trim(nc.ttipo)
                        Case 4
                            '--- Detalle Nota de credito por devolucion de envase
                            Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                            Texto = Texto + "C.U.                PRECIO                    IMPORTE" + vbCrLf

                            '---Detalle de la nc
                            For i As Integer = 0 To dtNc.Rows.Count - 1
                                item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                                If Not idproductoFl = item.litm.ToString Then
                                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                                    Texto = Texto + "@"
                                    Texto = Texto + objimpresion.AlinIzq(item.litm.ToString, 10)
                                    Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                                End If
                                Select Case item.idRubro
                                    Case "E"
                                        importeEnvase = importeEnvase + item.importe
                                    Case "C"
                                        importeCaja = importeCaja + item.importe
                                End Select
                                Texto = Texto + item.idRubro
                                Texto = Texto + "  " + Trim(item.trqt)
                                Texto = Texto + .AlinDer(FormatCurrency(item.precio * (1 + item.iva), 2), 20)
                                Texto = Texto + .AlinDer(FormatCurrency(item.importe, 2), 28) + vbCrLf
                                idproductoFl = item.litm.ToString
                                'Se agrego corte
                                Texto = Texto + "@"
                                If i = times Then
                                    times = times + 5
                                    Texto = Texto + "@"
                                End If

                            Next

                            Texto = Texto + "-------------------------------------------------------" + vbCrLf
                        Case 5
                            '--- Detalle Nota de credito por Descuento Pronto Pago
                            Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                            Texto = Texto + "C.U.                %DESC                    DESCUENTO" + vbCrLf
                            Texto = Texto + "-------------------------------------------------------" + vbCrLf
                            '---Detalle de la nc
                            For i As Integer = 0 To dtNc.Rows.Count - 1
                                item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                                Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                                Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                                Texto = Texto + "  " + Trim(item.trqt)
                                Texto = Texto + .AlinDer(FormatPercent(item.porcentajeDesto / 100, 2), 21)
                                Texto = Texto + .AlinDer(FormatCurrency(item.importeDesto, 2), 28) + vbCrLf
                                Texto = Texto + "-------------------------------------------------------" + vbCrLf
                                Texto = Texto + "@"
                            Next

                    End Select

                    '--- Pie de la nc
                    Select Case Trim(nc.ttipo)
                        Case 4
                            Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                            Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
                            Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
                        Case 5
                            Texto = Texto + "DESCUENTO TOTAL                         " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                            Texto = Texto + "D E S C U E N T O(-)                    " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                    End Select

                    'Texto = Texto + "No. FACTURA QUE ORIGINA ESTA N.C. " & docAsociado & " " & FormatDateTime(nc.fechaEmision.ToString, DateFormat.ShortDate) & vbCrLf
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    'Texto = Texto + "N.C. AUTORIZADA SEGUN RESOLUCION " + vbCrLf

                    'Texto = Texto + "NO.:" + Trim(nc.noResolucion) + "DE FECHA: " + FormatDateTime(nc.fechaResolucion, DateFormat.ShortDate) + vbCrLf
                    'Texto = Texto + "Serie: " + nc.serie + "  Del " + nc.inicial + " al " + nc.final + vbCrLf
                    Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
                    Texto = Texto + objimpresion.AlinCent("Admin : " + nc.serie + "-" + nc.numero, mAncho) + vbCrLf
                    Texto = Texto + "Sujeto a pagos trimestrales " + vbCrLf
                    Texto = Texto + "AGENTE DE RETENCION DEL IVA " + vbCrLf
                    Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
                    Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf

                    Texto = Texto + vbCrLf
                    Texto = Texto + vbCrLf
                    texto_1 = Texto
                    texto_2 = Texto
                    'Texto = Texto + "---------------------------------------------" + vbCrLf
                    'Texto = Texto + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                    'Texto = Texto + vbCrLf
                    'Texto = Texto + vbCrLf

            End With

            '--- Crear la copia en archivo de texto
            Select Case Trim(nc.ttipo)
                Case 4
                    textToFile("NCE_" + nc.serie + nc.numero + ".txt", Texto)
                Case 5
                    textToFile("NCDP_" + nc.serie + nc.numero + ".txt", Texto)
            End Select

            If preguntaSiImprimir Then
                If Not objimpresion.ConfirmaImpresion("Nota de credito") Then
                    objDocumento.numeroImpresiones(idNc, "NC", 0)
                    Return False
                End If
            End If

            texto_1 = texto_1 + "@"
            texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
            texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
            texto_1 = texto_1 + vbCrLf
            texto_1 = texto_1 + vbCrLf

            Dim arreglo As String()
            arreglo = texto_1.ToString.Split("@")

            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                texto_2 = texto_2 + "@"
                texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
                texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
                texto_2 = texto_2 + vbCrLf
                texto_2 = texto_2 + vbCrLf
                arreglo = texto_2.ToString.Split("@")
                objimpresion.Imprime_Documento_grande(arreglo)
                objDocumento.numeroImpresiones(idNc, "NC", 1)
                Return True
            Else
                objDocumento.numeroImpresiones(idNc, "NC", 0)
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function ImprimeNotaCreditoSINFEL(ByVal idNc As String, ByVal preguntaSiImprimir As Boolean, ByVal docAsociado As String) As Boolean

        Dim Texto As String = ""
        Dim nc As New documentoCO
        Dim cliente As ClienteCO
        Dim item As ItemCO
        Dim dtNc As DataTable
        Dim importeCaja, importeEnvase As Decimal
        Dim idproductoFl As String = ""
        Dim texto_1 As String = ""
        Dim texto_2 As String = ""
        Dim Ruta As New RutaCO
        Dim objRuta As New RutaBL



        Ruta = objRuta.getActiva()

        dtNc = objDocumento.getNotaCreditoDetalle(idNc)
        nc = objDocumento.getNotaCredito(idNc)
        cliente = objCliente.getDetalleDelCliente(nc.idCliente)

        With objimpresion
            .Imprime_Encabezado_doc(Texto)
            Texto = Texto + objimpresion.AlinCent("N O T A  D E  C R E D I T O", mAncho) + vbCrLf
            Texto = Texto + objimpresion.AlinCent("Serie: " + nc.serie + "  No.: " + nc.numero, mAncho) + vbCrLf

            '--- Tipo de nota de credito
            Select Case Trim(nc.ttipo)
                Case 4
                    Texto = Texto + .AlinCent("P O R  E N V A S E  R E C I B I D O", mAncho) + vbCrLf
                Case 5
                    Texto = Texto + .AlinCent("POR DESCUENTO PRONTO PAGO", mAncho) + vbCrLf
            End Select
            Texto = Texto + "FECHA     : " + Format(CDate(nc.fechaEmision), "dd/MM/yyyy") & vbCrLf
            If Ruta.clienteGenerico = cliente.codigo Then
                If (nc.doTipo = "ZTAE") Then
                    Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                    Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                    Texto = Texto + "NEGOCIO   : --N/A-- " + vbCrLf
                Else
                    Texto = Texto + "NOMBRE    : CONSUMIDOR FINAL " + vbCrLf
                    Texto = Texto + "NIT       : C/F " + vbCrLf
                    Texto = Texto + "NEGOCIO   : CONSUMIDOR FINAL " + vbCrLf
                End If

            Else
                Texto = Texto + "NOMBRE    : " + LTrim(RTrim(cliente.propietario)) + vbCrLf
                Texto = Texto + "NIT       : " + .AlinIzq(cliente.nit, 50) + vbCrLf
                Texto = Texto + "NEGOCIO   : " + .partir_textoI(LTrim(RTrim(cliente.negocio)), 45)
            End If
            Texto = Texto + "DIRECCION Y LUGAR DE ENTREGA: " + Trim(cliente.direccion) + vbCrLf
            Texto = Texto + "CATEGORIA : " + cliente.categoria + "               CODIGO: " + cliente.codigo + vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "@"
            Select Case Trim(nc.ttipo)
                Case 4
                    '--- Detalle Nota de credito por devolucion de envase
                    Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                    Texto = Texto + "C.U.                PRECIO                    IMPORTE" + vbCrLf

                    '---Detalle de la nc
                    For i As Integer = 0 To dtNc.Rows.Count - 1
                        item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                        If Not idproductoFl = item.litm.ToString Then
                            Texto = Texto + "-------------------------------------------------------" + vbCrLf
                            Texto = Texto + "@"
                            Texto = Texto + objimpresion.AlinIzq(item.litm.ToString, 10)
                            Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                        End If
                        Select Case item.idRubro
                            Case "E"
                                importeEnvase = importeEnvase + item.importe
                            Case "C"
                                importeCaja = importeCaja + item.importe
                        End Select
                        Texto = Texto + item.idRubro
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + .AlinDer(FormatCurrency(item.precio * (1 + item.iva), 2), 20)
                        Texto = Texto + .AlinDer(FormatCurrency(item.importe, 2), 28) + vbCrLf
                        idproductoFl = item.litm.ToString
                        'Se agrego corte
                        Texto = Texto + "@"
                        If i = times Then
                            times = times + 5
                            Texto = Texto + "@"
                        End If
                    Next
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                Case 5
                    '--- Detalle Nota de credito por Descuento Pronto Pago
                    Texto = Texto + "COD.                PRODUCTO " + vbCrLf
                    Texto = Texto + "C.U.                %DESC                    DESCUENTO" + vbCrLf
                    Texto = Texto + "-------------------------------------------------------" + vbCrLf
                    '---Detalle de la nc
                    For i As Integer = 0 To dtNc.Rows.Count - 1
                        item = objDocumento.getNotaCreditoByItem(dtNc.Rows(i))
                        Texto = Texto + objimpresion.AlinIzq(item.idProducto.ToString, 10)
                        Texto = Texto + Trim(item.descripcion.ToString) + vbCrLf
                        Texto = Texto + "  " + Trim(item.trqt)
                        Texto = Texto + .AlinDer(FormatPercent(item.porcentajeDesto / 100, 2), 21)
                        Texto = Texto + .AlinDer(FormatCurrency(item.importeDesto, 2), 28) + vbCrLf
                        Texto = Texto + "-------------------------------------------------------" + vbCrLf
                        Texto = Texto + "@"
                    Next

            End Select

            '--- Pie de la nc
            Select Case Trim(nc.ttipo)
                Case 4
                    Texto = Texto + "T O T A L                               " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                    Texto = Texto + "DEPOSITO ENVASE                         " + .AlinDer(FormatCurrency(importeEnvase, 2), 15) + vbCrLf
                    Texto = Texto + "DEPOSITO CAJA                           " + .AlinDer(FormatCurrency(importeCaja, 2), 15) + vbCrLf
                Case 5
                    Texto = Texto + "DESCUENTO TOTAL                         " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
                    Texto = Texto + "D E S C U E N T O(-)                    " + .AlinDer(FormatCurrency(nc.importe, 2), 15) + vbCrLf
            End Select

            Texto = Texto + "No. FACTURA QUE ORIGINA ESTA N.C. " & docAsociado & " " & Format(CDate(nc.fechaEmision), "dd/MM/yyyy") & vbCrLf
            Texto = Texto + "-------------------------------------------------------" + vbCrLf
            Texto = Texto + "N.C. AUTORIZADA SEGUN RESOLUCION " + vbCrLf
            
            Texto = Texto + "NO.:" + Trim(nc.noResolucion) + "DE FECHA: " + Format(CDate(nc.fechaResolucion), "dd/MM/yyyy") + vbCrLf
            Texto = Texto + "Serie: " + nc.serie + "  Del " + nc.inicial + " al " + nc.final + vbCrLf
            Texto = Texto + "VENDEDOR:" + co_glo_vendedor + vbCrLf
            Texto = Texto + objimpresion.AlinCent("Admin : " + nc.serie + "-" + nc.numero, mAncho) + vbCrLf
            Texto = Texto + "Sujeto a pagos trimestrales " + vbCrLf
            Texto = Texto + "AGENTE DE RETENCION DEL IVA " + vbCrLf
            Texto = Texto + .AlinCent("CERTIFICADOR " & "GUATEFACTURAS SOCIEDAD ANONIMA", mAncho) + vbCrLf
            Texto = Texto + .AlinCent("NIT CERTIFICADOR " & "5640773 - 4", mAncho) + vbCrLf
            Texto = Texto + vbCrLf
            Texto = Texto + vbCrLf
            texto_1 = Texto
            texto_2 = Texto
            'Texto = Texto + "---------------------------------------------" + vbCrLf
            'Texto = Texto + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
            'Texto = Texto + vbCrLf
            'Texto = Texto + vbCrLf

        End With

        '--- Crear la copia en archivo de texto
        Select Case Trim(nc.ttipo)
            Case 4
                textToFile("NCE_" + nc.serie + nc.numero + ".txt", Texto)
            Case 5
                textToFile("NCDP_" + nc.serie + nc.numero + ".txt", Texto)
        End Select

        If preguntaSiImprimir Then
            If Not objimpresion.ConfirmaImpresion("Nota de credito") Then
                objDocumento.numeroImpresiones(idNc, "NC", 0)
                Return False
            End If
        End If

        texto_1 = texto_1 + "@"
        texto_1 = texto_1 + "---------------------------------------------" + vbCrLf
        texto_1 = texto_1 + "      FIRMA CLIENTE      [ORIGINAL CLIENTE]  " + vbCrLf
        texto_1 = texto_1 + vbCrLf
        texto_1 = texto_1 + vbCrLf

        Dim arreglo As String()
        arreglo = texto_1.ToString.Split("@")

        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            texto_2 = texto_2 + "@"
            texto_2 = texto_2 + "---------------------------------------------" + vbCrLf
            texto_2 = texto_2 + "      FIRMA CLIENTE      [COPIA COMERCIO]    " + vbCrLf
            texto_2 = texto_2 + vbCrLf
            texto_2 = texto_2 + vbCrLf
            arreglo = texto_2.ToString.Split("@")
            objimpresion.Imprime_Documento_grande(arreglo)
            objDocumento.numeroImpresiones(idNc, "NC", 1)
            Return True
        Else
            objDocumento.numeroImpresiones(idNc, "NC", 0)
            Return False
        End If
    End Function

    Public Sub pruebaImpresion()
        Dim Texto As String 'Texto para la impresión 
        Texto = "Feliz dia... " + co_glo_vendedor + " ILG " + Date.Today
        If objimpresion.Imprime_Documento(Texto) = 0 Then
            MsgBox("La impresora esta lista para usarse.")
        Else
            MsgBox("La impresora no esta disponible.")
        End If
    End Sub

    Public Function textToFile(ByVal fileName As String, ByVal texto As String) As Boolean
        Try
            Dim path As String = "\TransientStorage\" + fileName
            Directory.CreateDirectory("\TransientStorage")
            If File.Exists(path) = False Then
                ' Create a file to write to.
                File.Delete(path)
                Dim sw As StreamWriter = File.CreateText(path)
                sw.WriteLine(texto)
                sw.Flush()
                sw.Close()
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Function eliminarDirectorio() As Boolean
        Try
            Dim di As New DirectoryInfo("\TransientStorage")
            If di.Exists Then
                di.Delete(True)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class

