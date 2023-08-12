using static System.String;

namespace SkyCommerce.Loja.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateSensitiveInformation(this string part)
        {
            return part.AsSpan().TruncateSensitiveInformation();
        }

        public static string TruncateSensitiveInformation(this ReadOnlySpan<char> part)
        {
            var firstAndLastLetterBuffer = new char[2];
            var firstAndLastLetter = new Span<char>(firstAndLastLetterBuffer);

            if (part != "")
            {
                part.Slice(0, 1).CopyTo(firstAndLastLetter.Slice(0, 1));
                part.Slice(part.Length - 1).CopyTo(firstAndLastLetter.Slice(1));

                return Create(part.Length, firstAndLastLetterBuffer, (span, s) =>
                {
                    s.AsSpan(0, 1).CopyTo(span);
                    for (var i = 1; i < span.Length - 1; i++)
                    {
                        span[i] = '*';
                    }
                    s.AsSpan(s.Length - 1).CopyTo(span.Slice(span.Length - 1));
                });
            }
            else
            {
                return "";
            }

        }
    }
}
