using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Migrations
{
    public partial class VieMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EnvioNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    CorreoValidado = table.Column<bool>(type: "bit", nullable: false),
                    IdValidacionCorreo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaEnvioValidacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaltContrasenna = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HashContrasenna = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Intentos = table.Column<int>(type: "int", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoInidicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DentroDelTramo = table.Column<bool>(type: "bit", nullable: false),
                    Desde = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Hasta = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoInidicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoInidicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlertaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Alertas_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "Alertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Bloqueado", "Celular", "Correo", "CorreoValidado", "Edad", "EnvioNewsletter", "FechaEnvioValidacion", "HashContrasenna", "IdValidacionCorreo", "Intentos", "NombreCompleto", "SaltContrasenna" },
                values: new object[] { new Guid("b3eef3ec-3fb5-4025-8490-b92ae7f4336f"), false, null, "flavio.luna@bittobyte.cl", true, 33, false, new DateTime(2020, 12, 10, 10, 41, 38, 208, DateTimeKind.Local).AddTicks(6420), "xEZGXRDuFMHMP7gp2fBOgEv4hIezygVHSfyFMt5zprU=", new Guid("109aa977-8a82-432e-a2b5-b4d5c5d9a20d"), 0, "Flavio Luna", "CzLFpSrR/Zj9hPUHvbR+fLQ6lkM=" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Bloqueado", "Celular", "Correo", "CorreoValidado", "Edad", "EnvioNewsletter", "FechaEnvioValidacion", "HashContrasenna", "IdValidacionCorreo", "Intentos", "NombreCompleto", "SaltContrasenna" },
                values: new object[] { new Guid("cec0a7bc-ea1d-4c9d-ac5f-457ff4cda1c4"), false, null, "maria.damianovic@bice.cl", false, 33, false, new DateTime(2020, 12, 10, 10, 41, 38, 216, DateTimeKind.Local).AddTicks(4641), "LPIHMyyoP9IHZys0rt5MYtMgNLu2/SwzHX2mnW0KRps=", new Guid("48e75466-d11c-4c58-8ec7-68204934956f"), 0, "Maria Eugenia Damianovic", "CzLFpSrR/Zj9hPUHvbR+fLQ6lkM=" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Bloqueado", "Celular", "Correo", "CorreoValidado", "Edad", "EnvioNewsletter", "FechaEnvioValidacion", "HashContrasenna", "IdValidacionCorreo", "Intentos", "NombreCompleto", "SaltContrasenna" },
                values: new object[] { new Guid("7649de4b-27fa-4a2e-a49b-d217b6a43368"), true, null, "bloqueado@bice.cl", true, 33, false, new DateTime(2020, 11, 10, 10, 41, 38, 216, DateTimeKind.Local).AddTicks(5112), "1y3DFxTWwM+ujIoM0N9IRo6XsYDuJ61XJPxtpQVeDu8=", new Guid("8493b383-5718-428a-b876-b774a7d1d749"), 0, "Usuario Bloqueado", "CzLFpSrR/Zj9hPUHvbR+fLQ6lkM=" });

            migrationBuilder.InsertData(
                table: "Favoritos",
                columns: new[] { "Id", "CodigoInidicador", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("16afa769-a30c-4c13-9bee-79cee2c4e312"), "uf", new Guid("b3eef3ec-3fb5-4025-8490-b92ae7f4336f") },
                    { new Guid("e92a53a8-23a5-457a-9925-c907bcc3ffa5"), "dolar", new Guid("b3eef3ec-3fb5-4025-8490-b92ae7f4336f") },
                    { new Guid("91cdb820-0380-4340-ace9-dd4291f0384c"), "euro", new Guid("b3eef3ec-3fb5-4025-8490-b92ae7f4336f") },
                    { new Guid("63f232e1-4c45-433a-8d4b-5b90691e1aee"), "tasa_desempleo", new Guid("cec0a7bc-ea1d-4c9d-ac5f-457ff4cda1c4") },
                    { new Guid("f2f3705c-99f5-49c9-af0a-7f4909f808ca"), "bitcoin", new Guid("cec0a7bc-ea1d-4c9d-ac5f-457ff4cda1c4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UsuarioId",
                table: "Alertas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_AlertaId",
                table: "Notificaciones",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
