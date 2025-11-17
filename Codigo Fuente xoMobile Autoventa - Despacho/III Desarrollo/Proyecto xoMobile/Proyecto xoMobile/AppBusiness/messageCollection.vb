Public Class messageCollection

    '--- Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult

    Public Function raiseMensaje(ByVal idMensaje As Integer, Optional ByVal extraDatos As String = "", Optional ByVal extraDatos2 As String = "") As MsgBoxResult
        getPartes(idMensaje, extraDatos, extraDatos2)
        Return response
    End Function

    Private Function getPartes(ByVal idMensaje As String, Optional ByVal extraDatos As String = "", Optional ByVal extraDatos2 As String = "") As MsgBoxResult
        Select Case idMensaje
            Case 100
                msg = "¿Desea liquidar el documento con pago de contado? " & vbCrLf & vbCrLf & "Su pago tiene un descuento de: " & extraDatos
                title = "Liquidacion de CXC"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)
            Case 200
                msg = "El total por liquido es de " & extraDatos & ". Su pago de contado aplica un descuento de: " & extraDatos2 & " Desea pagar  al contado?"
                title = "Venta"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 300
                msg = "Desea realizar el pago de contado?"
                title = "Pago de contado."
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 400
                msg = "¿Se compromete el cliente a pagar los documentos seleccionados en su proxima visita?"
                title = "Liquidacion de CXC"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 500
                msg = "¿Desea agregar una razon de no despacho para los items no marcados?"
                title = "Despachos"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 600
                msg = "No se permite " & extraDatos & " con saldo.  Cancele su deuda para continuar."
                title = "Atencion al cliente"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 700
                msg = "¿Desea confirmar la entrega de despachos?"
                title = "Despacho"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)

            Case 800
                msg = "Se ha confirmado con exito la entrega de despachos."
                title = "Despacho"
                style = MsgBoxStyle.Information
                response = MsgBox(msg, style, title)
        End Select


        Return response
    End Function


End Class
