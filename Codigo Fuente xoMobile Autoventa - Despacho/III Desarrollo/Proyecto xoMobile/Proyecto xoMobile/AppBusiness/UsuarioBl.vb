Imports System.Data
Imports Proyecto_xoMobile_Packs

'---ok
Public Class UsuarioBl
    Dim oUsuario As New Usuario

    Public Function cargarUsuario(ByVal password As String) As UsuarioCo

        Dim dtUsuario As New DataTable
        Dim vwUsuario As New DataView
        Dim usuario As New UsuarioCo
        Dim oRuta As New RutaBL
        Dim ruta As New RutaCO
        Try
            vwUsuario = oUsuario.getListado().DefaultView
            vwUsuario.RowFilter = "password = '" + password + "'"
            dtUsuario = vwUsuario.ToTable

            For i As Integer = 0 To dtUsuario.Rows.Count() - 1
                usuario.idUsuario = dtUsuario.Rows(i).Item("id_usuario")
                usuario.nombre = dtUsuario.Rows(i).Item("nombre")
                usuario.password = dtUsuario.Rows(i).Item("password")
                usuario.rol = dtUsuario.Rows(i).Item("rol")

                '--- Variables globales
                id_glo_usuario = usuario.idUsuario
                id_glo_rol = usuario.rol
                
                '--- Inicializar variables globales
                ruta = oRuta.getActiva()

                id_glo_fel = ruta.fel


                Select Case usuario.rol
                    Case "VEND"
                        co_glo_vendedor = usuario.nombre
                    Case "LIQU"
                        co_glo_liquidador = usuario.nombre
                    Case "SUPE"
                        co_glo_vendedor = usuario.nombre
                    Case "BODE"
                        co_glo_bodeguero = usuario.nombre
                End Select
            Next
            Return usuario
        Catch ex As Exception
            usuario.idUsuario = 0
            Return usuario
        End Try
    End Function

    Public Function login(ByVal clave As String, ByVal aplicacion As String) As Boolean
        Dim oBitacora As New BitacoraBL
        Dim usuario As New UsuarioCo
        usuario = cargarUsuario(clave)

        Select Case aplicacion

            Case Is = "BRINDIS"
                '--- Login de vendedor y supervisor
                If usuario.rol = "VEND" Or usuario.rol = "SUPE" Then
                    Return True
                End If

                '--- Login liquidador

                If (usuario.rol = "LIQU" Or usuario.rol = "LIQP") And oBitacora.isOperacionRealizada_xo(38) Then
                    Return True
                End If
            Case Is = "BODEGA"
                If usuario.rol = "BODE" Then
                    Return True
                End If
            Case Is = "LIQUIDACION"
                If usuario.rol = "LIQU" Or usuario.rol = "LIQP" Then
                    Return True
                End If

            Case Is = "SINCRONIZACION"
                If usuario.rol = "LIQU" Or usuario.rol = "LIQP" Or usuario.rol = "SUPE" Then
                    Return True
                End If

            Case Is = "ANULACION"
                If usuario.rol = "LIQU" Or usuario.rol = "LIQP" Then
                    Return True
                End If

            Case Else
                Return False
        End Select
        Return False
    End Function
End Class
