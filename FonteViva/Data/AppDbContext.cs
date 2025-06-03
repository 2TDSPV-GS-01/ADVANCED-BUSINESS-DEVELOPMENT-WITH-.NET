using FonteViva.Models;
using Microsoft.EntityFrameworkCore;

namespace FonteViva.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
        // Adicionar as outras classes 

        public DbSet<Fornecedor> Fornecedors { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Responsavel> Responsavels { get; set; }
        public DbSet<EstacaoTratamento> EstacaoTratamentos { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<RegistroMedida> RegistroMedidas { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("SEQ_ID_CONTATO")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("SEQ_ID_ENDERECO")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("SEQ_ID_MATERIAL")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("SEQ_ID_ESTACAO")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("SEQ_ID_SENSOR");

            modelBuilder.Entity<Contato>(entity =>
            {
                entity.ToTable("T_FV_CONTATO");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID_CONTATO")
                      .HasDefaultValueSql("SEQ_ID_CONTATO.NEXTVAL");

                entity.Property(e => e.Telefone)
                      .HasColumnName("DS_TELEFONE")
                      .HasMaxLength(14)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnName("DS_EMAIL")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.CPF)
                      .HasColumnName("DS_CPF")
                      .HasMaxLength(11);

                entity.Property(e => e.CNPJ)
                      .HasColumnName("DS_CNPJ")
                      .HasMaxLength(14);

                entity.HasOne(e => e.Responsavel)
                      .WithMany(r => r.Contatos)
                      .HasForeignKey(e => e.CPF)
                      .HasPrincipalKey(r => r.CPF)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Fornecedor)
                      .WithMany(f => f.Contatos)
                      .HasForeignKey(e => e.CNPJ)
                      .HasPrincipalKey(f => f.CNPJ)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("T_FV_ENDERECO");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnName("ID_ENDERECO")
                .HasDefaultValueSql("SEQ_ID_ENDERECO.NEXTVAL");

                entity.Property(e => e.Pais)
                .HasColumnName("DS_PAIS")
                .HasMaxLength(32)
                .IsRequired();

                entity.Property(e => e.Estado)
                .HasColumnName("DS_ESTADO")
                .HasMaxLength(64)
                .IsRequired();

                entity.Property(e => e.Cidade)
                .HasColumnName("DS_CIDADE")
                .HasMaxLength(64)
                .IsRequired();

                entity.Property(e => e.Rua)
                .HasColumnName("DS_RUA")
                .IsRequired();

                entity.Property(e => e.CEP)
                .HasColumnName("DS_CEP")
                .HasMaxLength(8)
                .IsRequired();

            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.ToTable("T_FV_FORNECEDOR");

                entity.HasKey(e => e.CNPJ);

                entity.Property(e => e.CNPJ)
                .HasColumnName("DS_CNPJ")
                .HasMaxLength(14);

                entity.Property(e => e.Nome)
                .HasColumnName("NM_FORNECEDOR")
                .IsRequired();

                entity.Property(e => e.IdEndereco)
                .HasColumnName("ID_ENDERECO")
                .IsRequired();

                entity.HasOne(f => f.Endereco)
                .WithOne(e => e.Fornecedor)
                .HasForeignKey<Fornecedor>(f => f.IdEndereco)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("T_FV_MATERIAL");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnName("ID_MATERIAL")
                .HasDefaultValueSql("SEQ_ID_MATERIAL.NEXTVAL");

                entity.Property(e => e.Nome)
                .HasColumnName("NM_MATERIAL")
                .IsRequired();

                entity.Property(e => e.Tipo)
                .HasColumnName("TP_MATERIAL")
                .HasMaxLength(32)
                .IsRequired();

                entity.Property(e => e.Quantidade)
                .HasColumnName("NR_QUANT_ESTOQUE")
                .IsRequired();

                entity.Property(e => e.Preco)
                .HasColumnName("NR_PRECO_UNIDADE")
                .HasPrecision(10, 2)
                .IsRequired();

                entity.Property(e => e.CNPJ)
                .HasColumnName("DS_CNPJ")
                .HasMaxLength(14)
                .IsRequired();

                entity.HasOne(e => e.Fornecedor)
                .WithMany(f => f.Materials)
                .HasForeignKey(e => e.CNPJ)
                .HasPrincipalKey(f => f.CNPJ)
                .OnDelete(DeleteBehavior.Cascade);

            });


            modelBuilder.Entity<Responsavel>(entity =>
            {
                entity.ToTable("T_FV_RESPONSAVEL");

                entity.HasKey(e => e.CPF);

                entity.Property(e => e.CPF)
                .HasColumnName("DS_CPF")
                .HasMaxLength(11)
                .IsRequired();

                entity.Property(e => e.Nome)
                .HasColumnName("NM_RESPONSAVEL")
                .IsRequired();
            });

            modelBuilder.Entity<EstacaoTratamento>(entity =>
            {
                entity.ToTable("T_FV_ESTACAO_TRATAMENTO");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnName("ID_ESTACAO_TRATAMENTO")
                .HasDefaultValueSql("SEQ_ID_ESTACAO.NEXTVAL");

                entity.Property(e => e.DataInstalacao)
                .HasColumnName("DT_INSTALACAO")
                .HasColumnType("Date")
                .IsRequired();

                entity.Property(e => e.Status)
                .HasColumnName("ST_ESTACAO")
                .HasDefaultValue("I")
                .IsRequired();

                entity.Property(e => e.CPF)
                .HasColumnName("DS_CPF")
                .HasMaxLength(11);

                entity.HasOne(e => e.Responsavel)
                .WithMany(r => r.EstacaoTratamentos)
                .HasForeignKey(e => e.CPF)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("T_FV_SENSOR");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnName("ID_SENSOR")
                .HasDefaultValueSql("SEQ_ID_SENSOR.NEXTVAL");

                entity.Property(e => e.TpSensor)
                .HasColumnName("TP_SENSOR")
                .HasMaxLength(32)
                .IsRequired();

                entity.Property(e => e.TpMedida)
                .HasColumnName("TP_MEDIDA")
                .HasMaxLength(10);

                entity.Property(e => e.IdEstacao)
                .HasColumnName("ID_ESTACAO_TRATAMENTO")
                .IsRequired();

                entity.HasOne(e => e.EstacaoTratamento)
                .WithMany(t => t.Sensors)
                .HasForeignKey(e => e.IdEstacao)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RegistroMedida>(entity =>
            {
                entity.ToTable("T_FV_REGISTRO_MEDIDA");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnName("ID_REGISTRO")
                .HasMaxLength(64)
                .IsRequired();

                entity.Property(e => e.DtRegistro)
                .HasColumnName("DT_REGISTRO")
                .IsRequired();

                entity.Property(e => e.Resultado)
                .HasColumnName("NR_RESULTADO")
                .HasPrecision(12, 4)
                .IsRequired();

                entity.Property(e => e.IdSensor)
                .HasColumnName("ID_SENSOR")
                .IsRequired();

                entity.HasOne(e => e.Sensor)
                .WithMany(s => s.RegistroMedidas)
                .HasForeignKey(e => e.IdSensor)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }

        //Adicionar as outras classes aqui
    }

}
