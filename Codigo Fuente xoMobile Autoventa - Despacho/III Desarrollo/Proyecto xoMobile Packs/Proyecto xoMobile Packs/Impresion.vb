Imports System
Imports System.IO.Ports
Imports System.IO
Imports System.Data
Imports System.Data.SqlServerCe


Public Class Impresion
    Public mAncho As Integer = 55 '40
    Dim Cadena_impresion As String
    'Dim Puerto As String
    Dim Velocidad As Long
    Dim objCe As New ceClient
    Dim Conexion As SqlCeConnection
    'Función que abre el puerto de la impresora configurada

    Public Function Abrir_Puerto() As System.IO.Ports.SerialPort

        If id_glo_puerto = Nothing Then
            id_glo_puerto = "COM9"

        End If

        Velocidad = 115000
        Dim ps As New System.IO.Ports.SerialPort(id_glo_puerto, Velocidad, Parity.None, 8, StopBits.One)
        Try
            If ps.IsOpen Then
                ps.Close()
            Else
                ps.Open()
            End If
            Return ps
        Catch ex As Exception
            Try
                id_glo_puerto = "COM4"
                Dim ps2 = New System.IO.Ports.SerialPort(id_glo_puerto, Velocidad, Parity.None, 8, StopBits.One)
                If ps2.IsOpen Then
                    ps2.Close()
                Else
                    ps2.Open()
                End If
                Return ps2
            Catch exl2 As Exception
                Try
                    id_glo_puerto = "COM2"
                    Dim ps2 = New System.IO.Ports.SerialPort(id_glo_puerto, Velocidad, Parity.None, 8, StopBits.One)
                    If ps2.IsOpen Then
                        ps2.Close()
                    Else
                        ps2.Open()
                    End If
                    Return ps2
                Catch exl3 As Exception
                    id_glo_puerto = Nothing
                    MsgBox("Si la HH es MC65 configure el puerto de impresion COM4 o COM2, de lo contrario configure el puerto de impresion COM9")
                    Return Nothing
                End Try
            End Try
        End Try
    End Function
    'Función que cierra el puerto despues de realizar la impresión 
    Public Function Cerrar_Puerto(ByVal Ps As System.IO.Ports.SerialPort) As Integer
        'Dim ps As New System.IO.Ports.SerialPort(Puerto, Velocidad, Parity.None, 8, StopBits.One)
        Try
            If Ps.IsOpen Then
                Ps.Close()
            End If
        Catch ex As Exception
            MsgBox("Error al cerrar puerto", MsgBoxStyle.Information)
            Return 1
        End Try
        Return 0
    End Function

    Public Function Espacios_en_blanco(ByRef Text As String) As String
        Text = Text + vbCrLf + vbCrLf + vbCrLf + vbCrLf
        Return Text
    End Function


    'Función que imprime el documento
    Public Function Imprime_Documento(ByVal Texto As String) As Integer
        Dim puerto As New System.IO.Ports.SerialPort
        Try
            puerto = Abrir_Puerto()
            If puerto.IsOpen Then
                puerto.WriteLine(Texto)
                If Cerrar_Puerto(puerto) = 1 Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 1
            End If
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Public Function Imprime_Documento_grande(ByVal Texto As String()) As Integer
        Dim puerto As New System.IO.Ports.SerialPort
        Dim caracteres As Integer = Texto.Length
        Dim veces As Integer = caracteres / 4000
        Try
            puerto = Abrir_Puerto()
            If puerto.IsOpen Then
                For i As Integer = 0 To Texto.Length - 1
                    puerto.Write(Texto(i).ToString)
                    Threading.Thread.Sleep(300)
                Next
                If Cerrar_Puerto(puerto) = 1 Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 1
            End If
        Catch ex As Exception
            Return 1
        End Try
    End Function
    'Función para confirmar impresiones 
    Public Function ConfirmaImpresion(ByVal preporte As String) As Boolean
        ConfirmaImpresion = (vbYes = MsgBox("" & preporte & vbCrLf _
                                            & "Desea Imprimir?", vbYesNo))
    End Function


    Public Function AlinCent(ByVal cadena As String, ByVal pancho As Integer) As String
        Dim l As Long
        Dim espacios As Integer
        Dim nueva As String
        Dim i As Integer
        l = Len(Trim(RTrim(cadena)))          'longitud real de la cadena
        nueva = Trim(RTrim(cadena))
        If l < pancho And l > 0 Then          'completo ancho con espacios
            'Si es mayor que el ancho de la impresora trunca el texto
            espacios = Int(((pancho - l) / 2))     'espacios que necesito para completar el ancho
            For i = 1 To espacios
                nueva = " " & nueva & " "
            Next i
        End If
        nueva = Mid(nueva, 1, pancho)
        Return nueva
    End Function
    '*******************************************************************
    'AJUSTA IMPRESION A LA IZQUIERDA
    '*******************************************************************
    Public Function AlinIzq(ByVal cadena As String, ByVal pancho As Integer) As String
        Dim l As Long
        Dim espacios As Long
        Dim nueva As String
        Dim i As Integer
        l = Len(cadena)                     'longitud real de la cadena
        If l < pancho And l > 0 Then        'completo ancho con espacios
            nueva = Trim(RTrim(cadena))
            espacios = pancho - l           'espacios que necesito para completar el ancho
            For i = 1 To espacios
                nueva = nueva & " "
            Next i
        Else                                'ajusto ancho al ancho deseado
            nueva = Left(cadena, pancho)
        End If
        AlinIzq = nueva
    End Function
    '*******************************************************************
    'AJUSTA IMPRESION A LA DERECHA
    '*******************************************************************
    Public Function AlinDer(ByVal cadena As String, ByVal pancho As Integer) As String
        Dim l As Integer
        Dim espacios As Integer
        Dim nueva As String
        Dim i As Integer
        l = Len(cadena)          'longitud real de la cadena
        nueva = ""
        If l < pancho And l > 0 Then        'completo ancho con espacios
            espacios = pancho - l     'espacios que necesito para completar el ancho
            For i = 1 To espacios
                nueva = nueva & " "
            Next i
            nueva = nueva & cadena
        Else                     'ajusto ancho al ancho deseado
            nueva = Left(cadena, pancho)
        End If
        AlinDer = nueva
    End Function

    'Función para partir texto y alinear al centro
    Public Function partir_textoC(ByVal texto As String, ByVal vancho As Integer) As String
        Dim vtext As String
        Dim returncadena As String
        Dim vtext_temp As String
        Dim vcadena As String
        Dim vlargo As Integer
        Dim vlargo1 As Integer
        Dim vlargo2 As Integer
        vtext_temp = Trim(LTrim(RTrim(texto)))
        vlargo = Len(vtext_temp)
        vtext = ""
        vcadena = ""
        returncadena = ""
        If vancho <= vlargo Then
            While vlargo >= 1 '(vancho / 2)
                vcadena = Mid(vtext_temp, 1, InStr(vtext_temp, " "))
                vlargo2 = Len(vcadena)
                vlargo1 = Len(vtext)
                If ((vlargo1 + vlargo2) <= (vancho)) And (vlargo2 > 0) Then
                    vtext = vtext + " " + vcadena
                    vtext_temp = Mid(vtext_temp, InStr(vtext_temp, " ") + 1, Len(vtext_temp))
                    vlargo = Len(vtext_temp)
                Else
                    If vlargo2 > 0 Then
                        returncadena = returncadena + AlinCent(vtext, vancho + 15) + vbCrLf
                        vlargo = Len(vtext_temp)
                    Else
                        returncadena = returncadena + AlinCent(vtext + vtext_temp, vancho + 15) + vbCrLf
                        vlargo = 0
                    End If
                    vtext = ""
                End If
            End While
        Else
            returncadena = vtext_temp + vbCrLf
        End If
        Return returncadena
    End Function

    'Función para partir texto y alinear ala izquierda
    Public Function partir_textoI(ByVal texto As String, ByVal vancho As Integer) As String
        Dim vtext As String
        Dim returncadena As String
        Dim vtext_temp As String
        Dim vcadena As String
        Dim vlargo As Integer
        Dim vlargo1 As Integer
        Dim vlargo2 As Integer
        vtext_temp = Trim(LTrim(RTrim(texto)))
        vlargo = Len(vtext_temp)
        vtext = ""
        vcadena = ""
        returncadena = ""
        If vancho <= vlargo Then
            While vlargo >= 1 '(vancho / 2)
                vcadena = Mid(vtext_temp, 1, InStr(vtext_temp, " "))
                vlargo2 = Len(vcadena)
                vlargo1 = Len(vtext)
                If ((vlargo1 + vlargo2) <= (vancho)) And (vlargo2 > 0) Then
                    vtext = vtext + " " + vcadena
                    vtext_temp = Mid(vtext_temp, InStr(vtext_temp, " ") + 1, Len(vtext_temp))
                    vlargo = Len(vtext_temp)
                Else
                    If vlargo2 > 0 Then
                        returncadena = returncadena + LTrim(RTrim(AlinIzq(vtext, vancho + 15))) + vbCrLf
                        vlargo = Len(vtext_temp)
                    Else
                        returncadena = returncadena + LTrim(RTrim(AlinIzq(vtext + vtext_temp, vancho + 15))) + vbCrLf
                        vlargo = 0
                    End If
                    vtext = ""
                End If
            End While
        Else
            returncadena = vtext_temp + vbCrLf
        End If
        Return returncadena
    End Function

    'Imprime los datos de cabecera de documentos
    Public Function Imprime_Encabezado_doc(ByRef vTexto As String) As Boolean
        Dim Texto As String 'Texto para la impresión 
        Dim SQL_QUERY As String 'Variable que contiene la consulta. 
        Dim dtEnc As DataTable 'Variable del tipo data Table
        Conexion = objCe.dbConnect()
        Conexion.Open()
        Texto = ""

        SQL_QUERY = " select 1, showvalue from ttipo where datavalue = 'NM' union " _
        + " select 2,showvalue from ttipo where datavalue = 'AB' union " _
        + " select 3, substring(showvalue,1,40) from ttipo where datavalue = 'DR' union " _
        + " select 4, substring(showvalue,42,100) from ttipo where datavalue = 'DR' union " _
        + " select 5,showvalue from ttipo where datavalue = 'TL' union " _
        + " select 6,showvalue from ttipo where datavalue = 'NI' "

        dtEnc = objCe.GetDataSet(SQL_QUERY)
        If dtEnc.Rows.Count > 0 Then
            Try
                Texto = Texto + AlinCent(Trim(dtEnc.Rows(0).Item(1).ToString), mAncho) + vbCrLf
                Texto = Texto + AlinCent(Trim(dtEnc.Rows(1).Item(1).ToString), mAncho) + vbCrLf
                Texto = Texto + AlinCent(Trim(dtEnc.Rows(2).Item(1).ToString), mAncho)
                Texto = Texto + AlinCent(Trim(dtEnc.Rows(3).Item(1).ToString), mAncho)
                Texto = Texto + AlinCent("Tel: " + Trim(dtEnc.Rows(4).Item(1).ToString) + "   NIT: " + Trim(dtEnc.Rows(5).Item(1).ToString), mAncho) + vbCrLf
                Imprime_Encabezado_doc = True
            Catch ex As Exception
                Imprime_Encabezado_doc = False
            End Try
            vTexto = Texto
        End If
    End Function
End Class

