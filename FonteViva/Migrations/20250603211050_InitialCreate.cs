using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FonteViva.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ID_CONTATO");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ID_ENDERECO");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ID_ESTACAO");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ID_MATERIAL");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_ID_SENSOR");

            migrationBuilder.CreateTable(
                name: "T_FV_ENDERECO",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_ID_ENDERECO.NEXTVAL"),
                    DS_PAIS = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: false),
                    DS_ESTADO = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    DS_CIDADE = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    DS_RUA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DS_CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_ENDERECO", x => x.ID_ENDERECO);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_RESPONSAVEL",
                columns: table => new
                {
                    DS_CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    NM_RESPONSAVEL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_RESPONSAVEL", x => x.DS_CPF);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_FORNECEDOR",
                columns: table => new
                {
                    DS_CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    NM_FORNECEDOR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_FORNECEDOR", x => x.DS_CNPJ);
                    table.ForeignKey(
                        name: "FK_T_FV_FORNECEDOR_T_FV_ENDERECO_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "T_FV_ENDERECO",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_ESTACAO_TRATAMENTO",
                columns: table => new
                {
                    ID_ESTACAO_TRATAMENTO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_ID_ESTACAO.NEXTVAL"),
                    ST_ESTACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false, defaultValue: "I"),
                    DT_INSTALACAO = table.Column<DateTime>(type: "Date", nullable: false),
                    DS_CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_ESTACAO_TRATAMENTO", x => x.ID_ESTACAO_TRATAMENTO);
                    table.ForeignKey(
                        name: "FK_T_FV_ESTACAO_TRATAMENTO_T_FV_RESPONSAVEL_DS_CPF",
                        column: x => x.DS_CPF,
                        principalTable: "T_FV_RESPONSAVEL",
                        principalColumn: "DS_CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_CONTATO",
                columns: table => new
                {
                    ID_CONTATO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_ID_CONTATO.NEXTVAL"),
                    DS_TELEFONE = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: true),
                    DS_CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_CONTATO", x => x.ID_CONTATO);
                    table.ForeignKey(
                        name: "FK_T_FV_CONTATO_T_FV_FORNECEDOR_DS_CNPJ",
                        column: x => x.DS_CNPJ,
                        principalTable: "T_FV_FORNECEDOR",
                        principalColumn: "DS_CNPJ",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_FV_CONTATO_T_FV_RESPONSAVEL_DS_CPF",
                        column: x => x.DS_CPF,
                        principalTable: "T_FV_RESPONSAVEL",
                        principalColumn: "DS_CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_MATERIAL",
                columns: table => new
                {
                    ID_MATERIAL = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_ID_MATERIAL.NEXTVAL"),
                    NM_MATERIAL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TP_MATERIAL = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: false),
                    NR_QUANT_ESTOQUE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NR_PRECO_UNIDADE = table.Column<float>(type: "BINARY_FLOAT(10)", precision: 10, scale: 2, nullable: false),
                    DS_CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_MATERIAL", x => x.ID_MATERIAL);
                    table.ForeignKey(
                        name: "FK_T_FV_MATERIAL_T_FV_FORNECEDOR_DS_CNPJ",
                        column: x => x.DS_CNPJ,
                        principalTable: "T_FV_FORNECEDOR",
                        principalColumn: "DS_CNPJ",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_SENSOR",
                columns: table => new
                {
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_ID_SENSOR.NEXTVAL"),
                    TP_SENSOR = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: false),
                    TP_MEDIDA = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    ID_ESTACAO_TRATAMENTO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_SENSOR", x => x.ID_SENSOR);
                    table.ForeignKey(
                        name: "FK_T_FV_SENSOR_T_FV_ESTACAO_TRATAMENTO_ID_ESTACAO_TRATAMENTO",
                        column: x => x.ID_ESTACAO_TRATAMENTO,
                        principalTable: "T_FV_ESTACAO_TRATAMENTO",
                        principalColumn: "ID_ESTACAO_TRATAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_FV_REGISTRO_MEDIDA",
                columns: table => new
                {
                    ID_REGISTRO = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    DT_REGISTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    NR_RESULTADO = table.Column<float>(type: "BINARY_FLOAT(12)", precision: 12, scale: 4, nullable: false),
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FV_REGISTRO_MEDIDA", x => x.ID_REGISTRO);
                    table.ForeignKey(
                        name: "FK_T_FV_REGISTRO_MEDIDA_T_FV_SENSOR_ID_SENSOR",
                        column: x => x.ID_SENSOR,
                        principalTable: "T_FV_SENSOR",
                        principalColumn: "ID_SENSOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_CONTATO_DS_CNPJ",
                table: "T_FV_CONTATO",
                column: "DS_CNPJ");

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_CONTATO_DS_CPF",
                table: "T_FV_CONTATO",
                column: "DS_CPF");

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_ESTACAO_TRATAMENTO_DS_CPF",
                table: "T_FV_ESTACAO_TRATAMENTO",
                column: "DS_CPF");

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_FORNECEDOR_ID_ENDERECO",
                table: "T_FV_FORNECEDOR",
                column: "ID_ENDERECO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_MATERIAL_DS_CNPJ",
                table: "T_FV_MATERIAL",
                column: "DS_CNPJ");

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_REGISTRO_MEDIDA_ID_SENSOR",
                table: "T_FV_REGISTRO_MEDIDA",
                column: "ID_SENSOR");

            migrationBuilder.CreateIndex(
                name: "IX_T_FV_SENSOR_ID_ESTACAO_TRATAMENTO",
                table: "T_FV_SENSOR",
                column: "ID_ESTACAO_TRATAMENTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_FV_CONTATO");

            migrationBuilder.DropTable(
                name: "T_FV_MATERIAL");

            migrationBuilder.DropTable(
                name: "T_FV_REGISTRO_MEDIDA");

            migrationBuilder.DropTable(
                name: "T_FV_FORNECEDOR");

            migrationBuilder.DropTable(
                name: "T_FV_SENSOR");

            migrationBuilder.DropTable(
                name: "T_FV_ENDERECO");

            migrationBuilder.DropTable(
                name: "T_FV_ESTACAO_TRATAMENTO");

            migrationBuilder.DropTable(
                name: "T_FV_RESPONSAVEL");

            migrationBuilder.DropSequence(
                name: "SEQ_ID_CONTATO");

            migrationBuilder.DropSequence(
                name: "SEQ_ID_ENDERECO");

            migrationBuilder.DropSequence(
                name: "SEQ_ID_ESTACAO");

            migrationBuilder.DropSequence(
                name: "SEQ_ID_MATERIAL");

            migrationBuilder.DropSequence(
                name: "SEQ_ID_SENSOR");
        }
    }
}
