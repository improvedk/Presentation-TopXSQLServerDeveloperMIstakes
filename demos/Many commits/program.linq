<Query Kind="Statements" />

using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=TopXDeveloperMistakes;Integrated Security=SSPI"))
{
	conn.Open();

	for (int i = 1; i <= 10000; i++)
	{
		SqlCommand cmd = new SqlCommand(@"
			INSERT INTO
				Images (Number, Size)
			VALUES
				(@Number, @Size)", conn);
		cmd.Parameters.Add("@Number", SqlDbType.SmallInt, i).Value = i;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = 5555;
		cmd.ExecuteNonQuery();

		if (i % 100 == 0)
			Console.WriteLine(i);
	}
}