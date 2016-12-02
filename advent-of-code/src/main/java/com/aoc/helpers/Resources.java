package com.aoc.helpers;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class Resources {
	private static final boolean IS_WINDOWS = System.getProperty( "os.name" ).contains( "indow" );
	
	public static String LoadTextFile(String path) throws IOException{
		//Get file from resources folder
		ClassLoader classLoader = Resources.class.getClassLoader();
		String absPath = classLoader.getResource(path).getPath();
		String osAppropriatePath = IS_WINDOWS ? absPath.substring(1) : absPath.toString();
		Path newPath = Paths.get(osAppropriatePath);
		
		return new String(Files.readAllBytes(newPath));
	}
}
