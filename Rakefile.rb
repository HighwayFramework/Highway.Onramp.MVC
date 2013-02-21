#!/usr/bin/env ruby

require 'albacore'
require 'fileutils'

CONFIG        = 'Debug'
RAKE_DIR      = File.expand_path(File.dirname(__FILE__))
SOLUTION_DIR  = RAKE_DIR + "/src"
TEMPLATE_DIR  = RAKE_DIR + "/Templates"
TEST_DIR      = SOLUTION_DIR + "/test/"
SRC_DIR       = SOLUTION_DIR + "/Highway.MVC/"
SOLUTION_FILE = 'Highway.sln'
TEMPLATE_FILE = 'Templates.MVC.sln'
MSTEST        = ENV['VS110COMNTOOLS'] + "..\\IDE\\mstest.exe"
NUGET         = SOLUTION_DIR + "/.nuget/nuget.exe"

task :default                     => ['build:mvc', 'build:data', 'build:logging', 'build:all']
task :test                        => ['build:mstest' ]
task :package                     => ['package:packall']
task :push                        => ['package:pushall']

namespace :build do

  def convert_to_pp(basepath, infile, outpath)
  	out_filename = outpath + infile + '.pp'
  	File.open(out_filename,'w+') do |output_file|
  		output_file.puts File.read(basepath + infile).gsub(/Templates\./,'$rootsnamespace$.')
  	end
	end
	
	task :mvc do
		create 'build_mvc/'
		files = [ 'App_Start/IoC.cs', 'Services/WindsorControllerFactory.cs', 'Installers/ControllerInstaller.cs', 'Installers/FilterInstaller.cs', 'Services/IoCFilterProvider.cs', 'App_Start/FilterProvidersWireup.cs', 'App_Start/ControllerFactoryWireup.cs' ]
		files.each do |file|
			convert_to_pp SRC_DIR, file, 'build_mvc/content/'
		end
		FileUtils.cp(SOLUTION_DIR + '/nuspec/Highway.Onramp.MVC.nuspec', RAKE_DIR + '/build_mvc/Highway.Onramp.MVC.nuspec')
		sh SOLUTION_DIR + '/.nuget/NuGet.exe pack ' + RAKE_DIR + '/build_mvc/Highway.Onramp.MVC.nuspec -o output'
	end
	
	task :all do
		create 'build_all/'
		FileUtils.cp(SOLUTION_DIR + '/nuspec/Highway.Onramp.MVC.All.nuspec', RAKE_DIR + '/build_all/Highway.Onramp.MVC.All.nuspec')
		sh SOLUTION_DIR + '/.nuget/NuGet.exe pack ' + RAKE_DIR + '/build_all/Highway.Onramp.MVC.All.nuspec -o output'
	end

		
	task :data do
		create 'build_data/'
		files = [ 'App_Start/DatabaseInitializerWireup.cs', 'Config/HighwayMappings.cs', 'Config/HighwayDataContext.cs', 'Config/HighwayContextConfiguration.cs', 'Config/HighwayDatabaseInitializer.cs', 'Installers/HighwayDataInstaller.cs' ]
		files.each do |file|
			convert_to_pp SRC_DIR, file, 'build_data/content/'
		end
		FileUtils.cp(SOLUTION_DIR + '/nuspec/Highway.Onramp.MVC.Data.nuspec', RAKE_DIR + '/build_data/Highway.Onramp.MVC.Data.nuspec')
		sh SOLUTION_DIR + '/.nuget/NuGet.exe pack ' + RAKE_DIR + '/build_data/Highway.Onramp.MVC.Data.nuspec -o output'
	end

	task :logging do
		create 'build_logging/'
		files = [ 'App_Start/LoggerAnnouncementsWireup.cs', 'BaseTypes/BaseLoggingController.cs', 'Filters/ExceptionLoggingFilter.cs', 'Installers/LoggingInstaller.cs', 'NLog.config'  ]
		files.each do |file|
			convert_to_pp SRC_DIR, file, 'build_logging/content/'
		end
		FileUtils.cp(SOLUTION_DIR + '/nuspec/Highway.Onramp.MVC.Logging.nuspec', RAKE_DIR + '/build_logging/Highway.Onramp.MVC.Logging.nuspec')
		sh SOLUTION_DIR + '/.nuget/NuGet.exe pack ' + RAKE_DIR + '/build_logging/Highway.Onramp.MVC.Logging.nuspec -o output'
	end
	
	def create(path)
		sh 'rm -rf ' + path + '/'
		Dir.mkdir(path)
		Dir.mkdir(path + 'lib')
		Dir.mkdir(path + 'tools')
		Dir.mkdir(path + 'content')
		Dir.mkdir(path + 'content/App_Start')
		Dir.mkdir(path + 'content/BaseTypes')
		Dir.mkdir(path + 'content/Services')
		Dir.mkdir(path + 'content/Config')
		Dir.mkdir(path + 'content/Installers')
		Dir.mkdir(path + 'content/Filters')
	end
end
