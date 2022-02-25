var blazorInterop = blazorInterop || {};

blazorInterop.showAlert = function (message) {
    alert(message);
};



blazorInterop.showPrompt = function (message, defaultValue) {
    return prompt(message, defaultValue);
};

blazorInterop.createBook = function (judul, penulis) {
    let book =
    {
        judul: judul,
        penulis: penulis,
        penerbit: "Cahaya Buku"

    };
    return book;
};



blazorInterop.focusElement = function (element) {
    element.focus();
};


blazorInterop.focusElementById = function (id) {
    var element = document.getElementById(id);
    if (element) element.focus();
};

blazorInterop.throwsError = function () {
    throw Error("Error from JS Function");
}

blazorInterop.methodCalculate = function () {
    var promise = DotNet.invokeMethodAsync("Blazor_JSInterop",
        "Calculate", 5);
    promise.then(result => alert(result));
};

blazorInterop.methodCalculateCustomIdentifier = function () {
    var promise = DotNet.invokeMethodAsync("Blazor_JSInterop",
        "CalculateWithVal2", 5, 6);
    promise.then(result => alert(result));
};

blazorInterop.sayHello1 = (dotNetHelper) => {
    return dotNetHelper.invokeMethodAsync('GetHelloMessage');
};

blazorInterop.callDotNetSetWindowSizeMethod = function (dotNetObjectRef) {
    dotNetObjectRef.invokeMethodAsync("SetWindowSize",
        {
            width: window.innerWidth,
            height: window.innerHeight
        });
};

blazorInterop.registerResizeHandler = function (dotNetObjectRef) {
    function resizeHandler() {
        dotNetObjectRef.invokeMethodAsync("SetWindowSize",
            {
                width: window.innerWidth,
                height: window.innerHeight
            });
    };

    // Set up initial values
    resizeHandler();

    // Register event handler
    window.addEventListener("resize", resizeHandler);
};