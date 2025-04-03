import { HomeIcon, Library, Plus, User } from 'lucide-react';

import Link from 'next/link';

import { IconButton } from '@/components/ui/icon-button';
import { ScrollArea } from '@/components/ui/scroll-area';
import { type Playlist } from '@/types';

import { SidebarItem } from './sidebar-item';
import { SidebarPlaylist } from './sidebar-playlist';

const playlists: Playlist[] = [
	{
		id: '1',
		title: 'Playlist 1',
		type: 'playlist',
		image: '/album.jpg',
		author: 'Author 1',
	},
	{
		id: '2',
		title: 'Playlist 2',
		type: 'playlist',
		image: '/album.jpg',
		author: 'Author 2',
	},
	{
		id: '3',
		title: 'Playlist 3',
		type: 'artist',
		image: '/album.jpg',
		author: 'Author 3',
	},
	{
		id: '4',
		title: 'Playlist 4',
		type: 'artist',
		image: '/album.jpg',
		author: 'Author 4',
	},
	{
		id: '5',
		title: 'Playlist 5',
		type: 'playlist',
		image: '/album.jpg',
		author: 'Author 5',
	},
	{
		id: '6',
		title: 'Playlist 6',
		type: 'playlist',
		image: '/album.jpg',
		author: 'Author 6',
	},
];

export const Sidebar = () => {
	return (
		<>
			<div className="space-y-2 rounded-xl bg-slate-900 p-2 pt-6 md:pt-2">
				<SidebarItem href="/" icon={HomeIcon} label="Home" />
				<SidebarItem href="/friends" icon={User} label="Friends" />
			</div>
			<div className="flex-1 rounded-xl bg-slate-900 p-2 md:px-4">
				<div className="mb-4 flex flex-col items-center justify-center gap-2 sm:flex-row sm:justify-between">
					<Link
						href="/library"
						aria-label="Library"
						title="Library"
						className="flex items-center px-2 text-white/80 hover:text-white"
					>
						<Library className="mr-2 size-5" aria-hidden="true" />
						<span className="hidden md:block">Library</span>
					</Link>
					<IconButton icon={Plus} label="Create playlist" className="text-white/80 hover:text-white" />
				</div>
				<ScrollArea className="h-[calc(100vh-300px)]">
					<div className="space-y-2">
						{playlists.map((playlist) => (
							<SidebarPlaylist key={playlist.id} playlist={playlist} />
						))}
					</div>
				</ScrollArea>
			</div>
		</>
	);
};
