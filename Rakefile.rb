#!/usr/bin/env ruby

require 'albacore'
require 'fileutils'

CONFIG        = 'Debug'
RAKE_DIR      = File.expand_path(File.dirname(__FILE__))
SOLUTION_DIR  = RAKE_DIR + "/Highway"
TEMPLATE_DIR  = RAKE_DIR + "/Templates"
TEST_DIR      = SOLUTION_DIR + "/test/"
SRC_DIR       = SOLUTION_DIR + "/src/"
SOLUTION_FILE = 'Highway.sln'
TEMPLATE_FILE = 'Templates.MVC.sln'
MSTEST        = ENV['VS110COMNTOOLS'] + "..\\IDE\\mstest.exe"
NUGET         = SOLUTION_DIR + "/.nuget/nuget.exe"

task :default                     => ['build:msbuild', 'build:templates']
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
		basePath = 'src/Templates.MVC/'
		files = [ 'App_Start/IoC.cs', 'Services/WindsorControllerFactory.cs', 'Installers/ControllerInstaller.cs', 'Installers/FilterInstaller.cs', 'Services/IoCFilterProvider.cs', 'App_Start/FilterProvidersWireup.cs', 'App_Start/ControllerFactoryWireup.cs' ]
		files.each do |file|
			convert_to_pp basePath, file, 'build_mvc/content/'
		end
	end
	
	task :logging do
		create 'build_logging/'
		basePath = 'src/Templates.MVC/'
		files = [ 'App_Start/LoggerAnnouncementsWireup.cs', 'BaseTypes/BaseLoggingController.cs', 'Filters/ExceptionLoggingFilter.cs', 'Installers/LoggingInstaller.cs', 'NLog.config'  ]
		files.each do |file|
			convert_to_pp basePath, file, 'build_logging/content/'
		end
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
