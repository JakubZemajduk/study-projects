namespace dotNETify.ModelsDTO
{
    public class PlaylistWithSongsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<SongDto> Songs { get; set; }
    }
}
