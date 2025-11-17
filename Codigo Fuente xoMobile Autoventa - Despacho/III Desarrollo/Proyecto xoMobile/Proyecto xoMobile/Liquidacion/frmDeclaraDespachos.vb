Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class frmDeclaraDespachos

#Region " DECLARACION DE VARIABLES Y OBJETOS "
    '---Objetos de Comunicacion entre procesos
    Public rLayer As New rLayerHandler
    Public realizado As Boolean = False

    '--- Objetos capa de negocio
    Dim oDespacho As New DespachoBL
    Dim msgCollection As New messageCollection

    '--- Variables
    Dim selectedIndex As Integer
    Dim real As Integer
    Dim proyectado As Integer
    Dim efectividad As Decimal


#End Region

    Private Sub frmDeclaraDespachos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dtDespachos As New DataTable
            Dim cliente As New ClienteCO
            Dim oBitacora As New BitacoraBL
            cliente.codigo = "%"

            'Obtener el listado de documentos a despachar
            dtDespachos = oDespacho.getDespachos(Cliente.codigo, rLayer)
            If rLayer.codigo = 0 Then
                crearListaDespachos(dtDespachos)
            Else
                Me.Close()
            End If

            '--- Le indica al sistema que el proceso ya fue iniciado por un usuario con acceso
            co_glo_despacho = True

            '--- Actualiza la operacion en bitacora - [Despacho Iniciado]
            oBitacora.registrarOperacion(49, id_glo_cliente)

            If realizado Then
                cmdConfirmar.Text = "Aceptar"
            End If
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
            Me.Close()
        End Try
    End Sub

    Private Sub crearListaDespachos(ByVal dtDespachos As DataTable)
        lstDespachos.Clear()

        Dim despachado = New ColumnHeader()
        Dim id = New ColumnHeader()
        Dim Documento = New ColumnHeader()
        Dim Monto = New ColumnHeader()
        Dim Litros = New ColumnHeader()
        Dim cliente = New ColumnHeader

        id.Text = "No."                '1
        Documento.Text = "Documento"   '2
        Monto.Text = "Monto"           '3
        Litros.Text = "Litros"         '4
        despachado.Text = "Despacho"       '5
        cliente.Text = "Cliente"

        lstDespachos.Columns.Add(id)
        lstDespachos.Columns.Add(Documento)
        lstDespachos.Columns.Add(Monto)
        lstDespachos.Columns.Add(Litros)
        lstDespachos.Columns.Add(despachado)
        lstDespachos.Columns.Add(cliente)


        For i As Integer = 0 To dtDespachos.Rows.Count - 1
            Dim drow As DataRow = dtDespachos.Rows(i)
            Dim lvi As New ListViewItem(i + 1.ToString())
            proyectado += 1

            lvi.SubItems.Add(drow("serie").ToString())
            lvi.SubItems.Add(FormatCurrency(drow("valor").ToString(), 2))
            lvi.SubItems.Add(drow("litros").ToString)

            '--- Marcar el item checked si ya fue despachado
            If drow("despachado").ToString() = "True" Then
                lvi.SubItems.Add("SI")
                real += 1
            ElseIf drow("despachado").ToString() = "False" Or drow("despachado").ToString = "" Then
                lvi.SubItems.Add("NO")
                lvi.BackColor = xoWarning
            End If
            lvi.SubItems.Add(drow("negocio").ToString)
            lstDespachos.Items.Add(lvi)
        Next
        lblNefectividad.Text = FormatPercent((real) / proyectado, 2)
        lblNentregados.Text = real
        lblNprogramados.Text = proyectado
    End Sub

#Region " CONFIRMAR DESPACHOS "
    Private Sub cmdConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmar.Click
        If cmdConfirmar.Text = "Aceptar" Then
            Me.Close()
            Exit Sub
        End If
        If msgCollection.raiseMensaje(700) = MsgBoxResult.Yes Then
            rLayer = oDespacho.confirmarDespachos()
            If rLayer.codigo = 0 Then
                msgCollection.raiseMensaje(800)
                Me.Close()
            End If
        End If
    End Sub
#End Region

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub
End Class