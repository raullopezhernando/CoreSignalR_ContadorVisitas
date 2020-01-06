
// Establecemos conexion con el end point (/contadorHub). Para el login utilizamos el level information y 
// con build construimos la conexion

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/contadorHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Iniciamos la conexion y metemos el catch para obtener los errores
connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Connection.on le permite a hub invocar un metodo en el cliente. Nos viene solo el parametro message desde el hub

connection.on("displayMessage", function (message) {
    var li = document.createElement("li");
    li.textContent = message;
    li.className = "list-group-item";
    document.getElementById("messages").appendChild(li);
});

// Codigo para interactuar con el metodo del hub "BroadcastMessage"
document.getElementById("send").addEventListener("click", function (event) {
    var message = document.getElementById("text").value;
    //Para invokar al Hub
    connection.invoke("BroadcastMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    // Una vez que se envia el mensaje borramos la caja de texto
    document.getElementById("text").value = "";
    //Habilitamos el evento preventDefault()
    event.preventDefault();
});