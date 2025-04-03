'use client';

import { Pause, Play } from 'lucide-react';

import { Button } from '@/components/ui/button';
import { cn } from '@/lib/utils';
import { usePlayerStore } from '@/store/use-player-store';

export const PlayButton = ({ songId }: { songId: string }) => {
	const { currentSong, isPlaying, togglePlay } = usePlayerStore();
	const isCurrentSong = currentSong?.id === songId;

	const handlePlay = () => {
		if (isCurrentSong) togglePlay();
		// else setCurrentSong(song);
	};

	return (
		<Button
			size={'icon'}
			onClick={handlePlay}
			className={cn(
				'absolute bottom-3 right-2 translate-y-2 rounded-full bg-brand opacity-0 transition-all hover:scale-105 hover:bg-violet-500 group-hover:translate-y-0',
				isCurrentSong ? 'opacity-100' : 'opacity-0 group-hover:opacity-100',
			)}
		>
			{isCurrentSong && isPlaying ? (
				<Pause className="size-5 fill-white text-white" />
			) : (
				<Play className="size-5 fill-white text-white" />
			)}
		</Button>
	);
};
