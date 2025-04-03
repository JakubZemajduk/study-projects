import { create } from 'zustand';

import { type Song } from '@/types';

interface PlayerStore {
	currentSong: Song | null;
	isPlaying: boolean;
	queue: Song[];
	currentIndex: number;
	volume: number;
	progress: number;
	repeatMode: 'none' | 'track' | 'queue';
	isShuffled: boolean;
	originalQueue: Song[];

	initializeQueue: (songs: Song[]) => void;
	playPlaylist: (songs: Song[], startIndex?: number) => void;
	setCurrentSong: (song: Song | null) => void;
	togglePlay: () => void;
	playNext: () => void;
	playPrevious: () => void;
	setVolume: (volume: number) => void;
	setProgress: (progress: number) => void;
	toggleRepeatMode: () => void;
	toggleShuffle: () => void;
}

export const usePlayerStore = create<PlayerStore>((set, get) => ({
	currentSong: null,
	isPlaying: false,
	queue: [],
	currentIndex: -1,
	volume: 1,
	progress: 0,
	repeatMode: 'none',
	isShuffled: false,
	originalQueue: [],

	initializeQueue: (songs: Song[]) => {
		set({
			queue: songs,
			currentSong: get().currentSong || songs[0],
			currentIndex: get().currentIndex === -1 ? 0 : get().currentIndex,
		});
	},

	playPlaylist: (songs: Song[], startIndex = 0) => {
		if (songs.length === 0) return;

		const song = songs[startIndex];

		set({
			queue: songs,
			currentSong: song,
			currentIndex: startIndex,
			isPlaying: true,
		});
	},

	setCurrentSong: (song: Song | null) => {
		if (!song) return;

		const songIndex = get().queue.findIndex((s) => s.id === song.id);
		set({
			currentSong: song,
			isPlaying: true,
			currentIndex: songIndex !== -1 ? songIndex : get().currentIndex,
		});
	},

	togglePlay: () => {
		const willStartPlaying = !get().isPlaying;

		set({
			isPlaying: willStartPlaying,
		});
	},

	setVolume: (volume: number) => {
		set({ volume: Math.max(0, Math.min(1, volume)) });
	},

	setProgress: (progress: number) => {
		set({ progress });
	},

	toggleRepeatMode: () => {
		const modes: ('none' | 'track' | 'queue')[] = ['none', 'track', 'queue'];
		const currentIndex = modes.indexOf(get().repeatMode);
		const nextMode = modes[(currentIndex + 1) % modes.length];
		set({ repeatMode: nextMode });
	},

	toggleShuffle: () => {
		const { queue, isShuffled, originalQueue } = get();

		if (isShuffled) {
			set({ queue: [...originalQueue], isShuffled: false });
		} else {
			set({
				originalQueue: [...queue],
				queue: [...queue].sort(() => Math.random() - 0.5),
				isShuffled: true,
			});
		}
	},

	playNext: () => {
		const { currentIndex, queue, repeatMode } = get();
		const nextIndex = currentIndex + 1;

		if (nextIndex < queue.length) {
			const nextSong = queue[nextIndex];

			set({
				currentSong: nextSong,
				currentIndex: nextIndex,
				isPlaying: true,
			});
		} else if (repeatMode === 'queue') {
			const firstSong = queue[0];
			set({
				currentSong: firstSong,
				currentIndex: 0,
				isPlaying: true,
			});
		} else if (repeatMode === 'track') {
			const currentSong = queue[currentIndex];
			set({
				currentSong: currentSong,
				isPlaying: true,
			});
		} else {
			set({ isPlaying: false });
		}
	},

	playPrevious: () => {
		const { currentIndex, queue, repeatMode } = get();
		const prevIndex = currentIndex - 1;

		if (prevIndex >= 0) {
			const prevSong = queue[prevIndex];

			set({
				currentSong: prevSong,
				currentIndex: prevIndex,
				isPlaying: true,
			});
		} else if (repeatMode === 'queue') {
			const lastSong = queue[queue.length - 1];
			set({
				currentSong: lastSong,
				currentIndex: queue.length - 1,
				isPlaying: true,
			});
		} else if (repeatMode === 'track') {
			const currentSong = queue[currentIndex];
			set({
				currentSong: currentSong,
				isPlaying: true,
			});
		} else {
			set({ isPlaying: false });
		}
	},
}));
