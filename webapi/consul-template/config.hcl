# This denotes the start of the configuration section for Consul. All values
# contained in this section pertain to Consul.
consul {
  address = "consul:8500"

  retry {
    enabled = true
    attempts = 12
    backoff = "125ms"
    max_backoff = "1m"
  }

}

reload_signal = "SIGKILL"
kill_signal = "SIGINT"
max_stale = "10m"
log_level = "debug"

# This is the quiescence timers; it defines the minimum and maximum amount of
# time to wait for the cluster to reach a consistent state before rendering a
# template. This is useful to enable in systems that have a lot of flapping,
# because it will reduce the the number of times a template is rendered.
wait {
  min = "5s"
  max = "10s"
}

exec {
  command = "launch.cmd"
  splay = "5s"
  kill_signal = "SIGTERM"
  kill_timeout = "2s"
}

# This block defines the configuration for a template. Unlike other blocks,
# this block may be specified multiple times to configure multiple templates.
# It is also possible to configure templates via the CLI directly.
template {
  source = "/app/consul-info.ctmpl"
  destination = "/app/consul-info.json"
  command = "powershell Get-Process | Where-Object {$_.Path -like \"*dotnet*\"} | Stop-Process"
  command_timeout = "5s"
  perms = 0777
  backup = true
  wait {
    min = "2s"
    max = "10s"
  }
}
