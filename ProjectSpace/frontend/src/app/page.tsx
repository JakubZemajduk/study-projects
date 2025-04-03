import { AlbumList } from '@/components/shared/album-list';
import { ArtistList } from '@/components/shared/artist-list';
import { GenreList } from '@/components/shared/genre-list';

export default function HomePage() {
	return (
		<div className="space-y-6 rounded-xl bg-slate-800/50 p-6">
			<GenreList />
			<AlbumList />
			<ArtistList />
		</div>
	);
}
