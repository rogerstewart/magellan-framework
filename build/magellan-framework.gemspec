version = File.read(File.expand_path("../VERSION",__FILE__)).strip  
  
Gem::Specification.new do |spec|  
  spec.platform    = Gem::Platform::RUBY  
  spec.name        = 'magellan-framework'  
  spec.version     = version  
  spec.files       = Dir['lib/**/*']  
  
  spec.summary     = 'Magellan - a navigation framework for WPF and Silverlight'
  spec.description = 'Magellan is a navigation framework for Windows Presentation Foundation and Silverlight. It helps you to build inductive, navigation-oriented applications. Features include routing, Model-View-Controller and Model-View-ViewModel.'

  spec.author            = 'Paul Stovell'  
  spec.email             = 'magellan-friends@googlegroups.com'  
  spec.homepage          = 'http://code.google.com/p/magellan-framework/'  
end
