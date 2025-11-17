Public Class felCabecera
    'Declaración de campos requeridos para el encabezado de la factura electronica
    Private _id As Integer
    Private _NITReceptor As String
    Private _CUIReceptor As String
    Private _tipoReceptor As String
    Private _idreceptor As String
    Private _nombre As String
    Private _direccion As String
    Private _tipoVenta As Char
    Private _destinoVenta As Integer '1=Guatemala
    Private _fecha As String
    Private _moneda As Integer '1=Quetzal 2= Dolar
    Private _tasa As Double
    Private _referencia As String
    Private _numeroAcceso As String
    Private _serieAdmin As String
    Private _numeroAdmin As Integer
    Private _reversion As Char
    Private _bruto As Double
    Private _descuento As Double
    Private _exento As Double
    Private _otros As Double
    Private _neto As Double
    Private _isr As Double
    Private _iva As Double
    Private _total As Double
    Private _numeroAbono As Double
    Private _fechaVencimiento As String
    Private _montoAbono As Double
    Private _vendedor As String
    Private _sociedad As Integer
    Private _tipoDocuemnto As String
    Private _tipoRuta As String
    Private _noPedido As String
    Private _serieFEL As String
    Private _preimpreso As String



    'Fin de la declaración de campos y variables

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property NITRECEPTOR() As String
        Get
            Return _NITReceptor
        End Get
        Set(ByVal value As String)
            _NITReceptor = value
        End Set
    End Property

    Public Property TIPORECEPTOR() As String
        Get
            Return _tipoReceptor
        End Get
        Set(ByVal value As String)
            _tipoReceptor = value
        End Set
    End Property

    Public Property IDRECEPTOR() As String
        Get
            Return _idreceptor
        End Get
        Set(ByVal value As String)
            _idreceptor = value
        End Set
    End Property

    Public Property CUIRECEPTOR() As String
        Get
            Return _CUIReceptor
        End Get
        Set(ByVal value As String)
            _CUIReceptor = value
        End Set
    End Property

    Public Property NOMBRE() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Public Property DIRECCION() As String
        Get
            Return _direccion
        End Get
        Set(ByVal value As String)
            _direccion = value
        End Set
    End Property

    Public Property TIPOVENTA() As Char
        Get
            Return _tipoVenta
        End Get
        Set(ByVal value As Char)
            _tipoVenta = value
        End Set
    End Property

    Public Property DESTINOVENTA() As Integer
        Get
            Return _destinoVenta
        End Get
        Set(ByVal value As Integer)
            _destinoVenta = value
        End Set
    End Property

    Public Property FECHA() As String
        Get
            Return _fecha
        End Get
        Set(ByVal value As String)
            _fecha = value
        End Set
    End Property

    Public Property MONEDA() As Integer
        Get
            Return _moneda
        End Get
        Set(ByVal value As Integer)
            _moneda = value
        End Set
    End Property

    Public Property TASA() As Double
        Get
            Return _tasa
        End Get
        Set(ByVal value As Double)
            _tasa = value
        End Set
    End Property


    Public Property REFERENCIA() As String
        Get
            Return _referencia
        End Get
        Set(ByVal value As String)
            _referencia = value
        End Set
    End Property

    Public Property NUMEROACCESO() As String
        Get
            Return _numeroAcceso
        End Get
        Set(ByVal value As String)
            _numeroAcceso = value
        End Set
    End Property

    Public Property SERIEADMIN() As String
        Get
            Return _serieAdmin
        End Get
        Set(ByVal value As String)
            _serieAdmin = value
        End Set
    End Property

    Public Property NUMEROADMIN() As Integer
        Get
            Return _numeroAdmin
        End Get
        Set(ByVal value As Integer)
            _numeroAdmin = value
        End Set
    End Property

    Public Property REVERSION() As Char
        Get
            Return _reversion
        End Get
        Set(ByVal value As Char)
            _reversion = value
        End Set
    End Property

    Public Property BRUTO() As Double
        Get
            Return _bruto
        End Get
        Set(ByVal value As Double)
            _bruto = value
        End Set
    End Property

    Public Property DESCUENTO() As Double
        Get
            Return _descuento
        End Get
        Set(ByVal value As Double)
            _descuento = value
        End Set
    End Property

    Public Property EXENTO() As Double
        Get
            Return _exento
        End Get
        Set(ByVal value As Double)
            _exento = value
        End Set
    End Property

    Public Property OTROS() As Double
        Get
            Return _otros
        End Get
        Set(ByVal value As Double)
            _otros = value
        End Set
    End Property

    Public Property NETO() As Double
        Get
            Return _neto
        End Get
        Set(ByVal value As Double)
            _neto = value
        End Set
    End Property

    Public Property ISR() As Double
        Get
            Return _isr
        End Get
        Set(ByVal value As Double)
            _isr = value
        End Set
    End Property

    Public Property IVA() As Double
        Get
            Return _iva
        End Get
        Set(ByVal value As Double)
            _iva = value
        End Set
    End Property

    Public Property TOTAL() As Double
        Get
            Return _total
        End Get
        Set(ByVal value As Double)
            _total = value
        End Set
    End Property

    Public Property NUMEROABONO() As Double
        Get
            Return _numeroAbono
        End Get
        Set(ByVal value As Double)
            _numeroAbono = value
        End Set
    End Property

    Public Property FECHAVENCIMIENTO() As String
        Get
            Return _fechaVencimiento
        End Get
        Set(ByVal value As String)
            _fechaVencimiento = value
        End Set
    End Property

    Public Property MONTOABONO() As Double
        Get
            Return _montoAbono
        End Get
        Set(ByVal value As Double)
            _montoAbono = value
        End Set
    End Property

    Public Property VENDEDOR() As String
        Get
            Return _vendedor
        End Get
        Set(ByVal value As String)
            _vendedor = value
        End Set
    End Property

    Public Property SOCIEDAD() As Integer
        Get
            Return _sociedad
        End Get
        Set(ByVal value As Integer)
            _sociedad = value
        End Set
    End Property

    
    Public Property TIPODOCUMENTO() As String
        Get
            Return _tipoDocuemnto
        End Get
        Set(ByVal value As String)
            _tipoDocuemnto = value
        End Set
    End Property

    Public Property TIPORUTA() As String
        Get
            Return _tipoRuta
        End Get
        Set(ByVal value As String)
            _tipoRuta = value
        End Set
    End Property

    Public Property NOPEDIDO() As String
        Get
            Return _noPedido
        End Get
        Set(ByVal value As String)
            _noPedido = value
        End Set
    End Property

    Public Property SERIEFEL() As String
        Get
            Return _serieFEL
        End Get
        Set(ByVal value As String)
            _serieFEL = value
        End Set
    End Property

    Public Property PREIMPRESO() As String
        Get
            Return _preimpreso
        End Get
        Set(ByVal value As String)
            _preimpreso = value
        End Set
    End Property


End Class

