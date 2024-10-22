using BrokerHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokerHub.Infrastructure.Persistence.Configuration;

public class ImovelConfiguration : IEntityTypeConfiguration<Imovel>
{
    public void Configure(EntityTypeBuilder<Imovel> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
               .ValueGeneratedOnAdd();

        builder.Property(i => i.Titulo)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(i => i.Area)
               .IsRequired();

        builder.Property(i => i.Tipo)
                .IsRequired();

        builder.Property(i => i.Preco)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(i => i.DataCriacao)
               .IsRequired();

        builder.Property(i => i.Status)
               .IsRequired();

        builder.OwnsOne(i => i.Endereco, endereco =>
        {
            endereco.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(150);

            endereco.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(10);

            endereco.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(100);

            endereco.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

            endereco.Property(e => e.CEP)
                    .IsRequired()
                    .HasMaxLength(10);

            endereco.Property(e => e.Complemento)
                    .HasMaxLength(25);
        });
    }
}