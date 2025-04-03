export interface Song {
	id: string;
	title: string;
	artist: string;
	albumId: string | null;
	imageUrl: string;
	audioUrl: string;
	duration: number;
	createdAt: string;
	updatedAt: string;
}

export interface Playlist {
	id: string;
	title: string;
	author: string;
	type: 'playlist' | 'artist';
	image: string;
}

export interface User {
	id: string;
	fullName: string;
	imageUrl: string;
}
