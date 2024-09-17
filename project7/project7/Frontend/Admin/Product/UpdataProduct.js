var x = localStorage.getItem("productId");
 const  url = `https://localhost:44381/api/Product/UpdateProduct/${x}`;

 var form = document.getElementById("form");
 async function updateProduct() {
     event.preventDefault();
     var formData = new FormData(form);
 
     var response = await fetch(url,{
        method: "PUT",
        body : formData 
     })
 
     alert("Product updated Successfully");
 
 
 }