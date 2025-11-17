
Public Class FelDetalle
    'Declaración de atributos para clase Detalle Factura
    Private _item As Integer
    Private _producto As Integer
    'Private _tipoProducto As Char
    Private _descripcion As String
    Private _medida As Integer
    Private _cantidad As Integer
    Private _precio As Double
    Private _porcDesc As Double
    Private _impBruto As Double
    Private _impDescuento As Double
    Private _impExento As Double
    Private _impOtros As Double
    Private _impNeto As Double
    Private _impIsr As Double
    Private _impIva As Double
    Private _impTotal As Double
    Private _tipoVentaDet As String
    Private _cantidadGravable As Double
    'Private _daSerie As Double
    'Private _daPreimpreso As Double
    'Private _subDetalle As FelDetalle

    Public Property ITEM() As Integer
        Get
            Return _item
        End Get
        Set(ByVal value As Integer)
            _item = value
        End Set
    End Property

    Public Property PRODUCTO() As Integer
        Get
            Return _producto
        End Get
        Set(ByVal value As Integer)
            _producto = value
        End Set
    End Property

    Public Property CANTIDADGRAVABLE() As Double
        Get
            Return _cantidadGravable
        End Get
        Set(ByVal value As Double)
            _cantidadGravable = value
        End Set
    End Property

    Public Property DESCRIPCION() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Public Property MEDIDA() As Integer
        Get
            Return _medida
        End Get
        Set(ByVal value As Integer)
            _medida = value
        End Set
    End Property

    Public Property CANTIDAD() As Integer
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Integer)
            _cantidad = value
        End Set
    End Property

    Public Property PRECIO() As Double
        Get
            Return _precio
        End Get
        Set(ByVal value As Double)
            _precio = value
        End Set
    End Property

    Public Property PORCDESC() As Double
        Get
            Return _porcDesc
        End Get
        Set(ByVal value As Double)
            _porcDesc = value
        End Set
    End Property

    Public Property IMPBRUTO() As Double
        Get
            Return _impBruto
        End Get
        Set(ByVal value As Double)
            _impBruto = value
        End Set
    End Property

    Public Property IMPDESCUENTO() As Double
        Get
            Return _impDescuento
        End Get
        Set(ByVal value As Double)
            _impDescuento = value
        End Set
    End Property

    Public Property IMPEXENTO() As Double
        Get
            Return _impExento
        End Get
        Set(ByVal value As Double)
            _impExento = value
        End Set
    End Property

    Public Property IMPOTROS() As Double
        Get
            Return _impOtros
        End Get
        Set(ByVal value As Double)
            _impOtros = value
        End Set
    End Property

    Public Property IMPNETO() As Double
        Get
            Return _impNeto
        End Get
        Set(ByVal value As Double)
            _impNeto = value
        End Set
    End Property

    Public Property IMPISR() As Double
        Get
            Return _impIsr
        End Get
        Set(ByVal value As Double)
            _impIsr = value
        End Set
    End Property

    Public Property IMPIVA() As Double
        Get
            Return _impIva
        End Get
        Set(ByVal value As Double)
            _impIva = value
        End Set
    End Property

    Public Property IMPTOTAL() As Double
        Get
            Return _impTotal
        End Get
        Set(ByVal value As Double)
            _impTotal = value
        End Set
    End Property

    Public Property TIPOVENTADET() As String
        Get
            Return _tipoVentaDet
        End Get
        Set(ByVal value As String)
            _tipoVentaDet = value
        End Set
    End Property

    'Public Property DASERIE() As Double
    '    Get
    '        Return _daSerie
    '    End Get
    '    Set(ByVal value As Double)
    '        _daSerie = value
    '    End Set
    'End Property

    'Public Property DAPREIMPRESO() As Double
    '    Get
    '        Return _dapreimpreso
    '    End Get
    '    Set(ByVal value As Double)
    '        _dapreimpreso = value
    '    End Set
    'End Property

    ' Public Property SubObject() As FelDetalle
    '     Get
    '         Return _subDetalle
    '     End Get
    '     Set(ByVal value As FelDetalle)
    '         _subDetalle = value
    '     End Set
    ' End Property

    'Public Sub New(ByVal PRODUCTO As Integer, ByVal DESCRIPCION As String, ByVal MEDIDA As Integer, ByVal CANTIDAD As Integer, ByVal PRECIO As Double, ByVal PORCDESC As Double, ByVal IMPBRUTO As Double, ByVal IMPDESCUENTO As Double, ByVal IMPEXENTO As Double, ByVal IMPOTROS As Double, ByVal IMPNETO As Double, ByVal IMPISR As Double, ByVal IMPIVA As Double, ByVal IMPTOTAL As Double, ByVal TIPOVENTADET As String)
    '   _producto = PRODUCTO
    '   _descripcion = DESCRIPCION
    '   _medida = MEDIDA
    '   _cantidad = CANTIDAD
    '  _precio = PRECIO
    '   _porcDesc = PORCDESC
    '    _impBruto = IMPBRUTO
    '    _impDescuento = IMPDESCUENTO
    '    _impExento = IMPEXENTO
    '    _impOtros = IMPOTROS
    '    _impNeto = IMPNETO
    '    _impIsr = IMPISR
    '    _impIva = IMPIVA
    '    _impTotal = IMPTOTAL
    '    _tipoVentaDet = TIPOVENTADET
    'End Sub


End Class

