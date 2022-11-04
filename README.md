# Store
Technical test, Fassil Bank

# Para levantar el ambiente
Primeramente se debe levantar la instancia de BD.
Se adjunta el script de creacion de BD, Tablas y Procedimientos almacenados (SP's).
**QueryDataBase.sql** en la raiz del proyecto.
Luego de la ejecucion del script de creacion de la BD, se deben modificar las cadenas de conexion de los servicios de Products y Sales (archivos appsettings.json) con los respectivos usuarios, contraseñas y servidor (si no esta corriendo en el mismo equipo).
Recordar que el usuario seleccionado para la cadena de conexion, debe tener los permisos suficientes sobre la nueva BD.
Para esta prueba no se manejo ningun tipo de encriptacion, por lo que la cadena de conexion va en texto plano.

Aunque es poco probable, pero de levantarse el microservicio de Productos y si esta corriendo en otro puerto distinto al 44388, se debe modificar la variable BaseUrlProductsMS del archivo de configuracion del servicio de Sales.

# Arquitecura

![Arquitectura y Diagrama de BD](https://drive.google.com/uc?export=view&id=175eC_j_5i7lHUplYAfjEkzXYyqmcyeIF)
# SwaggerUI
Ambos microservicios cuentan con swagger para la prueba de todos sus metodos.
métodos de operación HTTP (GET, POST, PUT, DELETE) son utilizados.



![Arquitectura y Diagrama de BD](https://drive.google.com/uc?export=view&id=136_HneLtykeIdzv1EgTph1S2y4tyVvN_)
# Pruebas
Como unico requisito para poder realizar flujos completos de prueba, se debe crear minimamente 1 producto (mediante el metodo POST).
Los demas metodos utilizan el ID de los productos, y validan su existencia, asegurece de que los productos existan antes de realizar operaciones como:

Obtener un producto especifico, crear una entrada de inventario, crear una actualizacion de inventario, crear una VENTA.

Cuando se crea un Producto, se crea con 0 stock.
Para incrementar el stock del producto se debe realizar un ingreso de productos (api/Products/RegisterEntry) indicando al proveedor (suplier) y ina lista de productos a ingresar al inventario.

Por Ejemplo(Json)

{
  "data": {
    "supplier": "MADEPA",
    "productEntryDetailDTOs": [
      {
        "quantity": 100,
        "idProduct": 1
      },
      {
        "quantity": 150,
        "idProduct": 2
      }
    ]
  }
}
# Registrar una VENTA
Al momento de registrar una venta, se crea una orden de compra.

Cuando se cambia el estado de la venta mediante el metodo de completar venta(api/sales/completeSale/{id}), se valida que existan suficientes itmes de los productos vendidos antes realizar cualquier operacion, si los productos existen, se cambia el estado de la venta a **E**
y luego se procede asincronamente a descontar esos productos del invetario (stock).
