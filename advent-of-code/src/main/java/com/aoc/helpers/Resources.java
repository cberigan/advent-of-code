package com.aoc.helpers;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class Resources {

	public static String LoadTextFile(String path) throws IOException{
		//Get file from resources folder
		ClassLoader classLoader = Resources.class.getClassLoader();
		return new String(Files.readAllBytes(Paths.get(classLoader.getResource(path).getPath())));
	}
}
