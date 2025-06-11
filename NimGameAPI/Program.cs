var builder = WebApplication.CreateBuilder(args);

// 1) Diz que vamos usar controllers (ou seja, nossa pasta Controllers)
builder.Services.AddControllers();

// 2) Para gerar documentação automática (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3) Se estivermos em desenvolvimento, habilita o Swagger no navegador
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();   // obriga HTTPS

app.MapControllers();        // “liga” nossas rotas definidas em Controllers

app.Run();                   // inicia o servidor


