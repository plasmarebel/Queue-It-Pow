public class Pow
{
    public static async Task<POWCompletion> GetHash(POWChallengeDetails challengeDetails)
        {
            var zeros = String.Concat(Enumerable.Repeat("0", challengeDetails.zeroCount));
            var postfix = 0;
            while (true)
            {
                string result;
                postfix += 1;
                var stri = challengeDetails.input + postfix.ToString();
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(stri));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    result = builder.ToString();
                }

                if (result.StartsWith(zeros))
                {
                    return new POWCompletion
                    {
                        hash = result,
                        postfix = postfix.ToString()
                    };
                }
            }
        }
}

public class POWChallengeDetails
    {
        public string sessionId { get; set; }
        public string meta { get; set; }
        public string input { get; set; }
        public int zeroCount { get; set; }
    }

    public class POWCompletion
    {
        public string hash { get; set; }
        public string postfix { get; set; }
    }
