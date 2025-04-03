import Image from 'next/image';

import { ScrollSection } from '@/components/ui/scroll-section';

interface Artist {
	id: string;
	name: string;
	image: string;
	type: string;
}

const artists: Artist[] = [
	{
		id: '1',
		name: 'Quebonafide',
		image: '/album.jpg',
		type: 'Artist',
	},
	{
		id: '2',
		name: 'sanah',
		image: '/album.jpg',
		type: 'Artist',
	},
	{
		id: '3',
		name: 'Mata',
		image: '/album.jpg',
		type: 'Artist',
	},
	{
		id: '4',
		name: 'bambi',
		image: '/album.jpg',
		type: 'Artist',
	},
	{
		id: '5',
		name: 'The Weeknd',
		image: '/album.jpg',
		type: 'Artist',
	},
	{
		id: '6',
		name: 'Billie Eilish',
		image: '/album.jpg',
		type: 'Artist',
	},
];

export const ArtistList = () => {
	return (
		<ScrollSection title="Popular artists" showAllHref="/artists">
			{artists.map((artist) => (
				<li
					key={artist.id}
					className="group relative w-[160px] space-y-4 rounded-md p-4 transition-colors hover:bg-neutral-800"
				>
					<div className="relative aspect-square">
						<Image
							src={artist.image}
							alt={artist.name}
							className="size-full rounded-full object-cover"
							width={300}
							height={300}
						/>
					</div>
					<div className="space-y-1 text-sm">
						<h3 className="font-semibold">{artist.name}</h3>
						<p className="text-neutral-400">Artist</p>
					</div>
				</li>
			))}
		</ScrollSection>
	);
};
