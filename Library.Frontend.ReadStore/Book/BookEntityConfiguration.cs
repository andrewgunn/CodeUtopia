using System.Data.Entity.ModelConfiguration;

namespace Library.Frontend.ReadStore.Book
{
    public class BookEntityConfiguration : EntityTypeConfiguration<BookEntity>
    {
        public BookEntityConfiguration()
        {
            ToTable("Book", "Book");

            HasKey(x => x.BookId);

            var columnOrder = 0;

            Property(p => p.BookId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.Title)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.IsBorrowed)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}