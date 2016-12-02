package com.aoc.problem2;

import java.io.IOException;

import com.aoc.helpers.Resources;

public class Problem2 {
	public static void main(String[] args) throws IOException {
		String[] moveSets = Resources.LoadTextFile("prob2.txt").split("\n");
		
		KeyPadEngine eng = new KeyPadEngine();
		
		for(String ms : moveSets){
			for(Character c : ms.toCharArray()){
				Move m = GetMove(c);
				if(m != null){
					eng.SubmitMove(m);
				}
			}
			eng.LogKeyNumber();
		}
		System.out.println(eng.GetCode());
	
	}
	
	private static Move GetMove(Character c){
		switch(c){
			case 'U': return Move.Up;
			case 'D': return Move.Down;
			case 'L': return Move.Left;
			case 'R': return Move.Right;
		}
		return null;
	}
}
