

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkMan.Domain.Entities;

namespace MilkMan.Infrastructure.DataAccess.Configurations
{
    public class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
    {

        public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
        {

            builder.HasData(
                  new MeasurementUnit { Id = 1, Name = "Liter", Symbol = "L" }, //Unit of volume for liquids
                  new MeasurementUnit { Id = 2, Name = "Kilogram", Symbol = "Kg" }, //Unit of mass for larger quantities
                  new MeasurementUnit { Id = 3, Name = "Gram", Symbol = "g" }, //Unit of mass for small quantities
                  new MeasurementUnit { Id = 4, Name = "Pound", Symbol = "lb" }, //Unit of mass, primarily used for for ghee
                  new MeasurementUnit { Id = 5, Name = "Milliliter", Symbol = "ml" }, //Unit of volume for small volume liquids
                  new MeasurementUnit { Id = 6, Name = "Each", Symbol = "each" }, //Individual unit of a fruit
                  new MeasurementUnit { Id = 7, Name = "Bunch", Symbol = "bunch" }, //Unit for specific fruits or vegetablessold in clusters
                  new MeasurementUnit { Id = 8, Name = "Dozen", Symbol = "doz" } //Unit of quantity equal to 12

                );


        }
    }
}
