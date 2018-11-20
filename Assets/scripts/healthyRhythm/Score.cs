using System;

[Serializable]
public class Score{
    public String songName;
    public int difficulty;
    public int beatz;
	public int miss;
	public int hit;
	public int good;
	public int perfect;
    public float percent;
    public Boolean finishedGame;

    public Score(String songName, int difficulty, int beatz, int miss, int hit, int good, int perfect, float percent, Boolean finishedGame){
        this.songName = songName;
        this.difficulty = difficulty;
        this.beatz = beatz;
        this.miss = miss;
		this.hit = hit;
		this.good = good;
		this.perfect = perfect;
        this.percent = percent;
        this.finishedGame = finishedGame;
	}
}


