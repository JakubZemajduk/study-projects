import Image from 'next/image';

import { ScrollSection } from '@/components/ui/scroll-section';

import { PlayButton } from './play-button';

interface Album {
	id: string;
	title: string;
	artist: string;
	image: string;
}

const albums: Album[] = [
	{
		id: '1',
		title: 'CHROMAKOPIA',
		artist: 'Tyler, The Creator',
		image: '/album.jpg',
	},
	{
		id: '2',
		title: 'Opowieści z Doliny Smoków',
		artist: 'Bedoes 2115, Lanek',
		image: '/album.jpg',
	},
	{
		id: '3',
		title: '100 dni do matury',
		artist: 'Mata',
		image: '/album.jpg',
	},
	{
		id: '4',
		title: 'W ZWIĄZKU Z MUZYKĄ',
		artist: 'Sobel',
		image: '/album.jpg',
	},
	{
		id: '5',
		title: 'Kaprysy',
		artist: 'sanah',
		image: '/album.jpg',
	},
	{
		id: '6',
		title: 'ERA47',
		artist: 'Oki',
		image: '/album.jpg',
	},
];

export const AlbumList = () => {
	return (
		<ScrollSection title="Popular singles" showAllHref="/songs">
			{albums.map((album) => (
				<li
					key={album.id}
					className="group relative w-[160px] space-y-4 rounded-md p-4 transition-colors hover:bg-neutral-800"
				>
					<div className="relative aspect-square">
						<Image
							src={album.image}
							alt={album.title}
							className="h-full w-full rounded-md object-cover"
							width={300}
							height={300}
						/>
						<PlayButton songId={album.id} />
					</div>
					<div className="space-y-1 text-sm">
						<h3 className="line-clamp-1 font-semibold">{album.title}</h3>
						<p className="line-clamp-2 text-neutral-400">{album.artist}</p>
					</div>
				</li>
			))}
		</ScrollSection>
	);
};
